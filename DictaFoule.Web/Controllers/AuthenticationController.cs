using DictaFoule.Common.Tools;
using DictaFoule.Web.Models.Authentication;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace DictaFoule.Web.Controllers
{
    public class AuthenticationController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!ValidateUser(model.Login, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Le nom d'utilisateur ou le mot de passe est incorrect.");
                return View(model);
            }

            var userClaims = new List<Claim>();
            userClaims.Add(new Claim(ClaimTypes.Email, model.Login));
            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, GetUserId(model.Login, model.Password)));
            var claimsIdentity = new ClaimsIdentity(userClaims, DefaultAuthenticationTypes.ApplicationCookie);

            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignIn(claimsIdentity);

            if (Url.IsLocalUrl(ViewBag.ReturnUrl))
                return Redirect(ViewBag.ReturnUrl);
            return RedirectToAction("Index", "Home");
        }

        private string GetUserId(string login, string password)
        {
            var md5password = Encrypt.GetMd5Hash(password);
            var User = entities.users.FirstOrDefault(u => u.email == login && u.password == md5password);
            return User.id.ToString();
        }

        private bool ValidateUser(string login, string password)
        {
            var md5password = Encrypt.GetMd5Hash(password);
            return entities.users.Any(u => u.email == login && u.password == md5password);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}