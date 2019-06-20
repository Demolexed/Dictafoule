using DictaFoule.API.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictafoule.Api.Test
{
    [TestFixture]
    public class ProjectControllerTest
    {
        private ProjectController projectController { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            projectController = new ProjectController();
        }


    }
}
