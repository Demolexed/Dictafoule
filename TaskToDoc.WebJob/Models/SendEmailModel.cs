using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskToDoc.WebJob.Models
{
    class SendEmailModel
    {
        public string GuidElements { get; set; }
        public string Email { get; set; }
        public int IdProject { get; set; }
    }
}
