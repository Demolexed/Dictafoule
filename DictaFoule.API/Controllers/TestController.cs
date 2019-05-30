using DictaFoule.API.Models.Test;
using System.Web.Http;

namespace DictaFoule.API.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet]
        [Route("v1/Test/Index")]
        public IHttpActionResult Index()
        {
            var test = new IndexModel
            {
                hello = "Hello world!"
            };
            return Ok(test);
        }
    }
}