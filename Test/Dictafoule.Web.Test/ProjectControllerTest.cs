using DictaFoule.Web.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictafoule.Web.Test
{
    [TestFixture]
    public class ProjectControllerTest
    {
        public ProjectControllerTest()
        {
            var toto = new ProjectController();
            toto.Index();
        }
    }
}
