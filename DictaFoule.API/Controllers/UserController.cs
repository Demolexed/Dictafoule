using DictaFoule.API.Models.Project;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DictaFoule.Common.DAL;
using DictaFoule.Common.Enum;
using DictaFoule.Common.Tools;

namespace DictaFoule.API.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        /// Recuperer les informations de l'utilisateur grâce au numéro d'identification du téléphone
        /// </summary>
        /// <param name="guidElements"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/User/GetUser")]
        public IHttpActionResult GetUser(string guidElements)
        {
            var user = entities.users.FirstOrDefault(a => a.guid == guidElements);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Creer un utilisateur
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/User/CreateUser")]
        public IHttpActionResult CreateUser(UserModel model)
        {
            try
            {
                var user = new user
                {
                    guid = model.Guid,
                    right = (int)RightAcces.User
                };
                entities.users.Add(user);
                entities.SaveChanges();
                LogTools.Add_log(LogLevel.INFO, " API CREATE USER", 0, "new user " + user.id);
                return Ok(true);
            }
            catch (Exception ex)
            {
                LogTools.Add_log(LogLevel.DANGER, " API CREATE USER", 0, "Fail create user " + ex.Message);
                return InternalServerError(ex);
            }
        }
    }
}