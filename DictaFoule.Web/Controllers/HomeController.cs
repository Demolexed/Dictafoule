using DictaFoule.Common.Enum;
using DictaFoule.Web.Models.Project;
using System.Linq;
using System.Web.Mvc;

namespace DictaFoule.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var projects = entities.logs.OrderByDescending(i => i.creation_date).Select(p => new LogModel
            {
                    IdLog = p.id_log,
                    Id_projet = p.id_project,
                    LogLevel = (LogLevel)p.id_log_level,
                    Message = p.message,
                    Type = p.type,
                    CreationDate = p.creation_date
            }).ToList();
            return View(projects);
        }
    }
}