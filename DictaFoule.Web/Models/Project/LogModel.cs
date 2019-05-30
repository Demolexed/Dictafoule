using DictaFoule.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DictaFoule.Web.Models.Project
{
    public class LogModel
    {
        public int Id_projet { get; set; }
        public int IdLog { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
    }
}