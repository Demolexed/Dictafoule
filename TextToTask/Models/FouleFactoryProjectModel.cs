using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TextToTask.WebJob
{
    class FouleFactoryProjectModel
    {
        [JsonProperty("IdProject")]
        public int IdProject { get; set; }

        [JsonProperty("IdState")]
        public int IdState { get; set; }

        [JsonProperty("IdTemplate")]
        public int? IdTemplate { get; set; }

        [JsonProperty("IdType")]
        public int? IdType { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("EstimatedTimePerTask")]
        public TimeSpan? EstimatedTimePerTask { get; set; }

        [JsonProperty("MaxEndDate")]
        public DateTime? MaxEndDate { get; set; }

        [JsonProperty("NbSupplierPerTask")]
        public int? NbSupplierPerTask { get; set; }

        [JsonProperty("AmountWithoutTaxPerTask")]
        public int? AmountWithoutTaxPerTask { get; set; }

        [JsonProperty("CsvFiles")]
        public List<int> CsvFiles { get; set; }

        [JsonProperty("TaskLines")]
        public List<int> TaskLines { get; set; }

        [JsonProperty("AutomaticValidation")]
        public bool AutomaticValidation { get; set; }

        [JsonProperty("IdCertification")]
        public int? IdCertification { get; set; }

        [JsonProperty("UrlNotification")]
        public string UrlNotification { get; set; }
    }
}
