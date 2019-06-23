using DictaFoule.API.Controllers;
using DictaFoule.API.Models.Project;
using DictaFoule.Common.DAL;
using DictaFoule.Common.Enum;
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

    
            string returnValue = "UklGRmQAAABXQVZFZm10IBAAAAABAAEAgD4AAAB9AAACABAAZGF0YUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";

            soundFileModel = new SoundFileModel
            {
                File64 = returnValue,
                Name = "zero.wav",
                Guid = "1a0c42b8-5465-4dbe-8660-45e22d8d38cd"
            };
        }

        [Test]
        public void CreateShouldReturnOkWav()
        {
            var response = projectController.Create(soundFileModel);

            var idproject = response as OkNegotiatedContentResult<int>;
            Assert.IsTrue(entities.projects.Any(p => p.id == idproject.Content), "Le projet n'as pas été ajouté à la DB");
            Assert.IsInstanceOf<OkNegotiatedContentResult<int>>(response, "La reponse n'est pas du type OkResult");
        }

        [Test]
        public void CreateShouldReturnOkMp3()
        {
            soundFileModel.Name = "zero.mp3";
            var response = projectController.Create(soundFileModel);

            var idproject = response as OkNegotiatedContentResult<int>;
            Assert.IsTrue(entities.projects.Any(p => p.id == idproject.Content), "Le projet n'as pas été ajouté à la DB");
            Assert.IsInstanceOf<OkNegotiatedContentResult<int>>(response, "La reponse n'est pas du type OkResult");
        }

        [Test]
        public void CreatShoulderNoUserFound()
        {
            soundFileModel.Guid = "no-user";
            var response = projectController.Create(soundFileModel);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(response, "La reponse n'est pas BadRequestErrorMessageResult");
        }

        [Test]
        public void CreatShoulderNullFile()
        {
            soundFileModel.File64 = null;
            var response = projectController.Create(soundFileModel);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(response, "La reponse n'est pas BadRequestErrorMessageResult");
        }

        [Test]
        public void CreatShoulderZeroLength()
        {
            soundFileModel.File64 = "";
            var response = projectController.Create(soundFileModel);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(response);
        }

        [Test]
        public void CreatShoulderWrongSoundFile()
        {
            soundFileModel.Name = "zero.txt";
            var response = projectController.Create(soundFileModel);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(response);
        }


        [TestCase(-1, "1a0c42b8-5465-4dbe-8660-45e22d8d38cd")]
        public void GetTransbribNoProject(int id_project, string guidElements)
        {
            var reponse = projectController.GetTransbrib(id_project, guidElements);

            Assert.IsInstanceOf<NotFoundResult>(reponse);
        }

        [TestCase(1, "no-guid")]
        public void GetTransbribNoGuidUser(int id_project, string guidElements)
        {
            var reponse = projectController.GetTransbrib(id_project, guidElements);

            Assert.IsInstanceOf<NotFoundResult>(reponse);
        }

        [TestCase(3, "44E2D28F-554E-48EE-A452-2B92CF9736FB")]
        public void GetTransbribTest(int id_project, string guidElements)
        {
            var reponse = projectController.GetTransbrib(id_project, guidElements);

            var stringcontent = reponse as OkNegotiatedContentResult<string>;
            Assert.IsTrue("Test123" == stringcontent.Content, "Le projet n'as pas été trouvé à la DB");
            Assert.IsInstanceOf<OkNegotiatedContentResult<string>>(reponse, "La reponse n'est pas du type OkResult");
        }

        [TestCase(-1, "1a0c42b8-5465-4dbe-8660-45e22d8d38cd")]
        public void GetStateProjectNoProject(int id_project, string guidElements)
        {
            var reponse = projectController.GetStateProject(id_project, guidElements);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(reponse);
        }

        [TestCase(1, "no-guid")]
        public void GetStateProjectWrongGuid(int id_project, string guidElements)
        {
            var reponse = projectController.GetStateProject(id_project, guidElements);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(reponse);
        }

        [TestCase(3, "44E2D28F-554E-48EE-A452-2B92CF9736FB")]
        public void GetStateProjectTest(int id_project, string guidElements)
        {
            var response = projectController.GetStateProject(id_project, guidElements);

            var idproject = response as OkNegotiatedContentResult<int>;
            Assert.IsTrue((int)ProjectState.ProjectCompleted == idproject.Content, "Le projet n'as pas été ajouté à la DB");
            Assert.IsInstanceOf<OkNegotiatedContentResult<int>>(response, "La reponse n'est pas du type OkResult");
        }

        [TestCase("zero.wav", "no-guid")]
        public void GetIdProjectWrongGuid(string nameFile, string guidElements)
        {
            var reponse = projectController.GetIdProject(nameFile, guidElements);

            Assert.IsInstanceOf<NotFoundResult>(reponse);
        }

        [TestCase("no-name", "1a0c42b8-5465-4dbe-8660-45e22d8d38cd")]
        public void GetIdProject(string nameFile, string guidElements)
        {
            var reponse = projectController.GetIdProject(nameFile, guidElements);

            Assert.IsInstanceOf<NotFoundResult>(reponse);
        }

        [TestCase("zero.wav", "1a0c42b8-5465-4dbe-8660-45e22d8d38cd")]
        public void GetIdProjectTest(string nameFile, string guidElements)
        {
            var response = projectController.GetIdProject(nameFile, guidElements);

            var idproject = response as OkNegotiatedContentResult<int>;
            Assert.IsTrue(entities.projects.Any(p => p.id == idproject.Content), "Le projet n'as pas été ajouté à la DB");
            Assert.IsInstanceOf<OkNegotiatedContentResult<int>>(response, "La reponse n'est pas du type OkResult");
        }
    }
}
