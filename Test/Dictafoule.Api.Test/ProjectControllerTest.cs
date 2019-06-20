using DictaFoule.API.Controllers;
using DictaFoule.API.Models.Project;
using DictaFoule.Common.DAL;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Dictafoule.Api.Test
{
    [TestFixture]
    public class ProjectControllerTest
    {
        private ProjectController projectController { get; set; }
        private SoundFileModel soundFileModel { get; set; }

        private Entities entities;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            entities = new Entities();
            projectController = new ProjectController();

            var toEncodeAsBytes = System.IO.File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory  + @"..\..\TranscriptionTest\zero.wav");
            var returnValue = Convert.ToBase64String(toEncodeAsBytes);

            soundFileModel = new SoundFileModel
            {
                File64 = returnValue,
                Name = "zero.wav",
                Guid = "1a0c42b8-5465-4dbe-8660-45e22d8d38cd"
            };
        }

        [Test]
        public void CreateShouldReturnOkWav(SoundFileModel soundFile)
        {
            var response = projectController.Create(soundFile);

            var idproject = response as OkNegotiatedContentResult<int>;
            Assert.IsTrue(entities.projects.Any(p => p.id == idproject.Content), "Le projet n'as pas été ajouté à la DB");
            Assert.IsInstanceOf<OkNegotiatedContentResult<int>>(response, "La reponse n'est pas du type OkResult");
        }

        [Test]
        public void CreateShouldReturnOkMp3(SoundFileModel soundFile)
        {
            soundFile.Name = "zero.mp3";
            var response = projectController.Create(soundFile);

            var idproject = response as OkNegotiatedContentResult<int>;
            Assert.IsTrue(entities.projects.Any(p => p.id == idproject.Content), "Le projet n'as pas été ajouté à la DB");
            Assert.IsInstanceOf<OkNegotiatedContentResult<int>>(response, "La reponse n'est pas du type OkResult");
        }



    }
}
