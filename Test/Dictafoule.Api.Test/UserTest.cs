using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using DictaFoule.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dictafoule.Api.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        [Description("Ceci test est un pour recuperer l'utilisateur")]
        public void GetUserTestShouldReturnNotFoundResult()
        {
            var controller = new UserController();
            var reponse = controller.GetUser("tutu");
            Assert.IsInstanceOfType(reponse, typeof(NotFoundResult));
        }
    }
}
