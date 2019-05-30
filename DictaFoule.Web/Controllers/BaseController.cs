using DictaFoule.Common.DAL;
using System.Security.Claims;
using System.Web.Mvc;

namespace DictaFoule.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly Entities entities;
        protected int idUser;

        protected BaseController()
        {
            entities = new Entities();
        }

        protected override void Dispose(bool disposing)
        {
            entities.Dispose();
            base.Dispose(disposing);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            idUser = GetUserId();
        }

        private int GetUserId()
        {
            var user = (ClaimsPrincipal)User;
            if (user.Identity.IsAuthenticated)
            {
                string stringIdUser = user.FindFirst(ClaimTypes.NameIdentifier).Value;
                int intIdUser;
                int.TryParse(stringIdUser, out intIdUser);
                return intIdUser;
            }
            return 0;
        }
    }
}