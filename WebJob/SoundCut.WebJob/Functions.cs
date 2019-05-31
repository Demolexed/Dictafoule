using System.IO;
using Microsoft.Azure.WebJobs;
using DictaFoule.Common.DAL;
using System;
using DictaFoule.Common.Enum;
using DictaFoule.Common.Tools;
using System.Net;
using System.Diagnostics;
using SoundCut;

namespace SoundCut.WebJob
{
    public class Functions
    {
        public static void ProcessQueueMessage([QueueTrigger("soundcut")] int id_project, TextWriter log)
        {
            log.WriteLine("SoundCut start - id_project: " + id_project);
            var errorMessage = string.Empty;
            var entities = new Entities();
            var project = entities.projects.Find(id_project);

            LogTools.Add_log(LogLevel.INFO, "SoundCut", id_project, "start Soundcut");

            if (project == null)
            {
                errorMessage += "no project with id_project: " + id_project;
                log.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }

            if (project.state != (int)ProjectState.SoundCut)
            {
                errorMessage += "wrong ProjectState for project :" + id_project + "State"+ (int)ProjectState.SoundCut;
                log.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }


            try
            {
                if (project.import_sound_file_name.Contains(".mp3"))
                {
                    var fileName = string.Format("project-{0}.mp3", project.id);
                    Stream stream = AzureBlobStorage.GetStream(fileName, "import");
                    CutSound.CutMp3(stream, project);
                }
                else
                {
                    Stream stream = AzureBlobStorage.GetStream(project.import_sound_file_name, "import");
                    CutSound.CutWav(stream, project);
                }
                ProjectTools.UpdateProjectState(id_project, ProjectState.SpeechToText);
                LogTools.Add_log(LogLevel.INFO, "SoundCut", id_project, "end Soundcut");
            }
            catch (Exception ex)
            {
                ProjectTools.UpdateProjectState(id_project, ProjectState.ErrorSoundCut);
                log.WriteLine(errorMessage + "Message: " + ex.Message);
                LogTools.Add_log(LogLevel.DANGER, "SoundCut", id_project, errorMessage + "Message" + ex.Message);
                throw ex;
            }

            log.WriteLine("SoundCut stop - id_project: " + id_project);
        }
    }
}
