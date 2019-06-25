using DictaFoule.Common.DAL;
using DictaFoule.Common.Enum;
using DictaFoule.Web.Controllers;
using DictaFoule.Web.Models.Project;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dictafoule.Web.Test
{
    [TestFixture]
    public class ProjectControllerTest
    {
        private ProjectController projectController { get; set; }
        private HttpPostedFileBase httpPostedFileBase { get; set; }

        private MyTestPostedFileBase myTestPostedFileBase { get; set; }

        private Entities entities;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            projectController = new ProjectController();
            entities = new Entities();

            var audiofile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"..\..\TranscriptionTest\zero.wav", FileMode.Open);
            myTestPostedFileBase = new MyTestPostedFileBase(audiofile, "audio/wave", "zero.mp3");

        }


        [Test]
        public void IndexTest()
        {
            var reponse = projectController.Index();

            var listProject = reponse as ViewResult;

            var viewModel = entities.projects.OrderByDescending(i => i.creation_date).Select(p => new ListModel
            {
                CreationDate = p.creation_date,
                FileName = p.import_sound_file_name,
                IdProject = p.id,
                ImportUri = p.import_sound_file_uri,
                State = (ProjectState)p.state
            }).ToList();

            var mylist = listProject.Model as List<ListModel>;

            Assert.AreEqual(viewModel.Count, mylist.Count);
        }


        [Test]
        [Ignore("Test cassé sur AzureDevops")]
        public void UploadTestNominal()
        {
            var reponse = projectController.Upload(myTestPostedFileBase);
            var result = reponse as JsonResult;
            var absoluteUri = result.Data;

            var uri = entities.projects.ToList().LastOrDefault();

            Assert.AreEqual(new { AbsoluteUri = uri.import_sound_file_uri }.ToString(), absoluteUri.ToString());
        }

        [Test]
        public void UploadTestFailFileNull()
        {
            myTestPostedFileBase = new MyTestPostedFileBase(new MemoryStream(), null, "zero.mp3");
            var reponse = projectController.Upload(myTestPostedFileBase);

            Assert.IsInstanceOf<HttpStatusCodeResult>(reponse);
        }

        [Test]
        public void UploadTestFailFileWrongFormat()
        {
            myTestPostedFileBase.SetFileName("zero.wav");
            var reponse = projectController.Upload(myTestPostedFileBase);

            Assert.IsInstanceOf<HttpStatusCodeResult>(reponse);
        }
    }
}
