using System;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;
using DictaFoule.API.Controllers;
using DictaFoule.API.Models.Project;
using DictaFoule.Common.DAL;
using NUnit.Framework;

namespace Dictafoule.Api.Test
{
    [TestFixture]
    public class UserTest
    {
        private UserController userController { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            userController = new UserController();
        }

        [TestCase("1a0c42b8-5465-4dbe-8660-45e22d8d38cd")]
        [Test]
        public void CreateUserShouldReturnOK(string guid)
        {
            var userModel = new UserModel()
            {
                Guid = guid
            };
            var response = userController.CreateUser(userModel);
            Assert.IsInstanceOf<OkNegotiatedContentResult<bool>>(response, "La reponse n'est pas du type OkResult");
        }

        [Test]
        [Description("Ceci test est un pour recuperer l'utilisateur")]
        public void GetUserTestShouldReturnNotFoundResult()
        {
            var reponse = userController.GetUser("lulu");
            Assert.IsInstanceOf<NotFoundResult>(reponse);
        }

        [Test]
        [Description("Ceci test est un pour recuperer l'utilisateur")]
        public void GetUserTestShouldReturnFound()
        {
            var reponse = userController.GetUser("1a0c42b8-5465-4dbe-8660-45e22d8d38cd");
            Assert.IsInstanceOf<OkNegotiatedContentResult<bool>>(reponse, "La reponse n'est pas du type OkResult");
        }
    }
}
