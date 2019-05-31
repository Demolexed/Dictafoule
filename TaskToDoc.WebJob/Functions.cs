using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using DictaFoule.Common.DAL;
using FouleFactoryApi.PCL;
using Newtonsoft.Json;
using System.Configuration;
using DictaFoule.Common.Enum;
using System.Data.Entity;
using TaskToDoc.WebJob.Models;
using DictaFoule.Common.Tools;
using System.Drawing;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace TaskToDoc.WebJob
{
    public class Functions
    {
        public static void ProcessQueueMessageAsync([QueueTrigger("tasktodoc")]int id_taskline, TextWriter log)
        {
            log.WriteLine("Start TaskToDoc");
            var errorMessage = "TaskToDoc error - ";
            var entities = new Entities();

            try
            {
                var fouleFactory = new FouleFactoryApiClient(ConfigurationManager.AppSettings["FOULEFACTORY_API_URL"], ConfigurationManager.AppSettings["FOULEFACTORY_API_LOGIN"], ConfigurationManager.AppSettings["FOULEFACTORY_API_PASSWORD"]);
                var taskAnswer = fouleFactory.Tasks.GetTasksGet(id_taskline);
                string taskAnswerString = taskAnswer.ToString();
                TaskResume answer = JsonConvert.DeserializeObject<TaskResume>(taskAnswerString);

                var soundlines = entities.sound_line.Where(a => a.id_taskline == answer.IdTaskLine).ToList();
                if (soundlines == null)
                {
                    errorMessage += "no soundlines with id_taskline: " + id_taskline;
                    log.WriteLine(errorMessage);
                    throw new Exception(errorMessage);
                }

                foreach (var sound_line in soundlines)
                {
                    sound_line.task_answer = answer.TaskAnswer.FirstOrDefault();
                    sound_line.state = (int)SoundLineState.Completed;
                    entities.Entry(sound_line).State = EntityState.Modified;
                    entities.SaveChanges();
                    LogTools.Add_log(LogLevel.INFO, "TaskToDoc", sound_line.id_project, "TaskToDoc succes " + id_taskline);
                }

                int idproject = soundlines.FirstOrDefault().id_project;
                var nbcompleted = entities.sound_line.Where(a => a.id_project == idproject && a.state == (int)SoundLineState.Completed).Count();
                var nbsoudline = (entities.sound_line.Where(a => a.id_project == idproject).Count());
                if (nbcompleted == nbsoudline)
                {
                    SendEmail(idproject);
                    ProjectTools.UpdateProjectState(idproject, ProjectState.ProjectCompleted);
                    LogTools.Add_log(LogLevel.INFO, "TaskToDoc", idproject, "end TaskToDoc");
                }
            }
            catch (Exception ex)
            {
                log.WriteLine(errorMessage + "Message: " + ex.Message);
                LogTools.Add_log(LogLevel.DANGER, "TaskToDoc", 1 , "id_taskline " + id_taskline + "Message : " + ex.Message);
                throw ex;
            }
        }

        private static void SendEmail(int idproject)
        {
            var entities = new Entities();
            var project = entities.projects.Find(idproject);
            var email = entities.orders.Where(p => p.id_project == idproject).ToList();
            var users = entities.users.Where(u => u.id == project.id_user).ToList();
            SendEmailModel sendEmailModel = new SendEmailModel() { IdProject = idproject, Email = email.FirstOrDefault().email, GuidElements = users.FirstOrDefault().guid };
            
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ff-prod-api-dictafoule.azurewebsites.net/v1/Project/SendEmail");
                request.Method = "POST";
                request.ContentType = "application/json";
                using (var stream = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(sendEmailModel);
                    stream.Write(json);
                }
                var response = request.GetResponse();
                LogTools.Add_log(LogLevel.INFO, "API SEND EMAIL", idproject, "Succes send email to " + sendEmailModel.Email);
            }
            catch (Exception ex)
            {
                LogTools.Add_log(LogLevel.DANGER, "API SEND EMAIL", idproject, "Fail send email to " + email.FirstOrDefault().email + " ,error :" + ex.Message);
            }
        }
    }
}
