using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskToDoc.WebJob.Models
{
    public class TaskResume
    {
        public int IdTask { get; set; }
        public int IdTaskLine { get; set; }
        public int IdSupplier { get; set; }
        public int IdTaskState { get; set; }
        public int IdTaskType { get; set; }
        public string UpdateDate { get; set; }
        public string CreateDate { get; set; }
        public List<string> TaskAnswer { get; set; }
    }
}
