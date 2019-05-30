using DictaFoule.Common.DAL;
using System.Web.Http;

namespace DictaFoule.API.Controllers
{
    public class BaseController : ApiController
    {
        protected readonly Entities entities;
        protected BaseController()
        {
            entities = new Entities();
        }

        protected override void Dispose(bool disposing)
        {
            entities.Dispose();
            base.Dispose(disposing);
        }
    }
}