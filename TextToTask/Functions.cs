using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Azure.WebJobs;
using FouleFactoryApi.PCL;
using System.Configuration;
using DictaFoule.Common.DAL;
using DictaFoule.Common.Tools;
using DictaFoule.Common.Enum;
using TextToTask.WebJob.Api;
using TextToTask.Models;

namespace TextToTask.WebJob
{
    public class Functions
    {
        public static void ProcessQueueMessage([QueueTrigger("texttotask")] int id_project, TextWriter log)
        {
            log.WriteLine("Start TexttoTask");
            var errorMessage = "TexttoTask error - ";           
            var entities = new Entities();
            var entitesproject = new Entities();

            LogTools.Add_log(LogLevel.INFO, "TextToTask", id_project, "start TextToTask");

            var project = entitesproject.projects.Find(id_project);
            var soundlines = entities.sound_line.Where(p => p.id_project == id_project).ToList();
            if (soundlines == null || project == null)
            {
                errorMessage += "no project with id project: " + id_project;
                log.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }
            if (project.state != (int)ProjectState.TextToTask)
            {
                errorMessage += "wrong ProjectState for id_project: " + id_project;
                log.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }

            try
            {
                int? idTemplate;
                int? idProject;
                int? idTaskLine;
                var foulefactory = new FouleFactoryApiClient(ConfigurationManager.AppSettings["FOULEFACTORY_API_URL"], ConfigurationManager.AppSettings["FOULEFACTORY_API_LOGIN"], ConfigurationManager.AppSettings["FOULEFACTORY_API_PASSWORD"]);

                idTemplate = Create.CreateTemplate(foulefactory);
                if (!idTemplate.HasValue)
                    throw new Exception("Template issue");
                log.WriteLine("New template created " + idTemplate.Value);

                idProject = Create.CreateProject(foulefactory, idTemplate.Value);
                if(!idProject.HasValue)
                    throw new Exception("Project issue");
                log.WriteLine("New project created " + idProject.Value);

                foreach (var sound in soundlines)
                {
                    idTaskLine = Create.AddTaskLines(foulefactory, sound.name, sound.transcript, idProject.Value);
                    if (!idTaskLine.HasValue)
                        throw new Exception("Task issue");
                    log.WriteLine("New task_line created " + idTaskLine.Value);
                    sound.id_taskline = idTaskLine.Value;
                    sound.state = (int)SoundLineState.Posted;
                    entities.Entry(sound).State = System.Data.Entity.EntityState.Modified;
                    entities.SaveChanges();
                    LogTools.Add_log(LogLevel.INFO, "TextToTask", id_project, "posted " + sound.id);
                }

                //var idcsv = Create.AddcsvFile(foulefactory, soundlines, idProject.Value);
                //if (!idcsv.HasValue)
                //    throw new Exception("CSV issue");
                //log.WriteLine("New csv created " + idcsv.Value);
                LogTools.Add_log(LogLevel.INFO, "TextToTask", id_project, "end TextToTask");

            }
            catch (Exception ex)
            {
                ProjectTools.UpdateProjectState(id_project, ProjectState.ErrorTextToTask);
                log.WriteLine(errorMessage + "Message: " + ex.Message);
                LogTools.Add_log(LogLevel.DANGER, "TextToTask", id_project, errorMessage + "Message" + ex.Message);
                throw ex;
            }
            log.WriteLine("TexttoTask stop - id_project: " + id_project);
        }
    }
}
