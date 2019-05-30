using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DictaFoule.API.Models.Project
{
    public class SendEmailModel
    {
        public string GuidElements { get; set; }
        public string Email { get; set; }
        public int IdProject { get; set; }
    }
}