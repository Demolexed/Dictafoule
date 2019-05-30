using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TextToTask.WebJob
{
    class FouleFactoryTaskModel
    {
        [JsonProperty("IdTask")]
        public int IdTask { get; set; }

        [JsonProperty("IdTaskLine")]
        public int? IdTaskLine { get; set; }

        [JsonProperty("IdSupplier")]
        public int? IdSupplier { get; set; }

        [JsonProperty("IdTaskState")]
        public int IdTaskState { get; set; }

        [JsonProperty("IdTaskType")]
        public int? IdTaskType { get; set; }

        [JsonProperty("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [JsonProperty("CreateDate")]
        public DateTime? CreateDate { get; set; }

        [JsonProperty("TaskAnswer")]
        public List<string> TaskAnswer { get; set; }
    }
}
