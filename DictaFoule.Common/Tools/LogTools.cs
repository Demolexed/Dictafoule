using DictaFoule.Common.DAL;
using DictaFoule.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictaFoule.Common.Tools
{
    public static class LogTools
    {
        public static void Add_log(LogLevel loglevel, string type, int id_project, string message)
        {
            var entities = new Entities();
            var log_db = new log
            {
                id_log_level = (int)loglevel,
                type = type,
                id_project = id_project,
                message = message,
                creation_date = DateTime.Now,
            };
            entities.logs.Add(log_db);
            entities.SaveChanges();
        }
    }
}
