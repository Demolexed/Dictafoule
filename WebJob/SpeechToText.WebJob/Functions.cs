using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using DictaFoule.Common.Enum;
using DictaFoule.Common.DAL;
using DictaFoule.Common.Tools;
using System.Net;

namespace SpeechToText.WebJob
{
    public class Functions
    {
        public static void ProcessQueueMessage([QueueTrigger("speechtotext")] int id_project, TextWriter log)
        {
            log.WriteLine("SpeechToText start - id_sound_line: " + id_project);
            var errorMessage = "SpeechToText error - ";
            var entities = new Entities();
            var soundlines = entities.sound_line.Where(a => a.id_project == id_project).ToList();
            var project = entities.projects.Find(id_project);

            LogTools.Add_log(LogLevel.INFO, "SpeechToText", id_project, "start SpeechToText");
            if (soundlines == null)
            {
                errorMessage += "no sound_line with id_sound_line: " + id_project;
                log.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }

            if (project.state != (int)ProjectState.SpeechToText)
            {
                errorMessage += "wrong ProjectState for id_project: " + id_project;
                log.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }

            try
            {

                foreach (var soundline in soundlines)
                {
                    if (soundline.state == (int)SoundLineState.Create)
                    {

                        SpeechmaticsClient speech = new SpeechmaticsClient();
                        var job = speech.CreateTranscriptionJob(soundline.sound_file_name, AzureBlobStorage.GetStream(soundline.sound_file_name, "soundline"), false);
                        var text = speech.getSpeech(job.Job);
                        soundline.sound_file_transcript = text;
                        soundline.state = (int)SoundLineState.Translate;
                        entities.Entry(soundline).State = System.Data.Entity.EntityState.Modified;
                        entities.SaveChanges();
                        LogTools.Add_log(LogLevel.INFO, "SpeechToText", soundline.id_project, "SpeechToText succes " + soundline.id_sound_line);
                        log.WriteLine("SpeechToText success - id_sound_line: " + id_project);
                    }
                }
                ProjectTools.UpdateProjectState(id_project, ProjectState.TextToTask);
                LogTools.Add_log(LogLevel.INFO, "SpeechToText", id_project, "end SpeechToText");
                AzureQueueStorage.QueueProject(id_project, "texttotask");
            }
            catch (Exception ex)
            {
                ProjectTools.UpdateProjectState(id_project, ProjectState.ErrorSpeechToText);
                log.WriteLine(errorMessage + "Message: " + ex.Message);
                LogTools.Add_log(LogLevel.DANGER, "SpeechToText", id_project, errorMessage + "Message" + ex.Message);
                throw ex;
            }

            log.WriteLine("SpeechToText stop - id_sound_line: " + id_project);
        }
    }
}
