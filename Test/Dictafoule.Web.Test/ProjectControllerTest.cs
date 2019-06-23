using DictaFoule.Common.DAL;
using DictaFoule.Common.Enum;
using DictaFoule.Web.Controllers;
using DictaFoule.Web.Models.Project;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
        private Entities entities;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            projectController = new ProjectController();
            entities = new Entities();
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

            Assert.AreSame(viewModel, listProject.ViewData.Model);
        }















        public ProjectControllerTest()
        {
            var toto = new ProjectController();
            toto.Index();
        }
    }
}
