using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DictaFoule.API.Controllers
{
    public class HealthCheckController : ApiController
    {
        [HttpGet]
        [Route("v1/Project/HealthChecking")]
        public IHttpActionResult HealthChecking()
        {
            return Ok();
        }
    }
}