//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DictaFoule.Common.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class sound_line
    {
        public int id { get; set; }
        public int id_project { get; set; }
        public string name { get; set; }
        public string uri { get; set; }
        public string transcript { get; set; }
        public System.DateTime creation_date { get; set; }
        public int state { get; set; }
        public Nullable<int> id_taskline { get; set; }
        public string task_answer { get; set; }
    
        public virtual project project { get; set; }
    }
}
