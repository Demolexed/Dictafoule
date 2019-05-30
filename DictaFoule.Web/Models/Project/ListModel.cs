using DictaFoule.Common.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DictaFoule.Web.Models.Project
{
    public class ListModel
    {
        public int IdProject { get; set; }
        public string FileName { get; set; }
        public string ImportUri { get; set; }
        public DateTime CreationDate { get; set; }
        public ProjectState State { get; set; }
    }
}