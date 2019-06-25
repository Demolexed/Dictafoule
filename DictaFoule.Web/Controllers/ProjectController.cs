using DictaFoule.Common.DAL;
using DictaFoule.Common.Enum;
using DictaFoule.Common.Tools;
using DictaFoule.Web.Models.Project;
using FouleFactoryApi.PCL;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Xceed.Words.NET;

namespace DictaFoule.Web.Controllers
{
    public class ProjectController : BaseController
    {
        public ActionResult Index()
        {
            var viewModel = entities.projects.OrderByDescending(i => i.creation_date).Select(p => new ListModel
            {
                CreationDate = p.creation_date,
                FileName = p.import_sound_file_name,
                IdProject = p.id,
                ImportUri = p.import_sound_file_uri,
                State = (ProjectState)p.state
            }).ToList();
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase ImportFile)
        {
            if (ImportFile == null)
                return new HttpStatusCodeResult(500, "Le fichier est null");
            if (ImportFile.ContentLength == 0)
                return new HttpStatusCodeResult(500, "Le fichier est vide");
            if (!DataValidation.IsMp3(ImportFile.FileName))
                return new HttpStatusCodeResult(500, "Le fichier n'est pas au bon format");

            var importFile = new project
            {
                creation_date = DateTime.UtcNow,
                state = (int)ProjectState.Upload,
                import_sound_file_name = ImportFile.FileName,
            };
            entities.projects.Add(importFile);
            entities.SaveChanges();

            var format = string.Empty;
            if (DataValidation.IsMp3(ImportFile.FileName))
                format = "mp3";

            var fileName = string.Format("project-{0}.{1}", importFile.id, format);
            var absoluteUri = AzureBlobStorage.Upload(ImportFile, fileName, "import");

            if (string.IsNullOrWhiteSpace(absoluteUri))
                return new HttpStatusCodeResult(500, "Un problème est survenu pendant l'upload du fichier");

            importFile.import_sound_file_uri = absoluteUri;
            entities.Entry(importFile).State = EntityState.Modified;
            entities.SaveChanges();

            ProjectTools.UpdateProjectState(importFile.id, ProjectState.SoundCut);
            AzureQueueStorage.QueueProject(importFile.id, "soundcut");

            return Json(new { AbsoluteUri = absoluteUri });
        }

        [AllowAnonymous]
        public ActionResult Notification(string name, int id, int state)
        {
            if (state == 4 && name == "task")
                AzureQueueStorage.QueueProject(id, "tasktodoc");
            ViewBag.IdTasks = "Notification";
            return View();
        }

        public FileResult Download(int id)
        {
            if (!entities.sound_line.Where(a => a.id_project == id).Any(a => a.task_answer == null))
            {
                TimeSpan time = new TimeSpan();
                time = TimeSpan.Zero;
                MemoryStream stream = new MemoryStream();
                DocX doc = DocX.Create(stream);
                var texts = entities.sound_line.Where(a => a.id_project == id).ToList();
                foreach (var text in texts)
                {
                    Paragraph par = doc.InsertParagraph();
                    par.Append("[" + time.ToString("g") + "]\n");
                    par.Append(text.task_answer).Font("Times New Roman");
                    doc.Save();
                    time = time.Add(TimeSpan.FromMinutes(1));
                }
                return File(stream.ToArray(), "application/octet-stream", texts[0].id_project + ".docx");
            }
            return null;
        }
    }
}