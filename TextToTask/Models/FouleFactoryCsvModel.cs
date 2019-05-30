

namespace TextToTask.Models
{
    class FouleFactoryCsvModel
    {
        public int IdCsvFile { get; set; }
        public int IdProject { get; set; }
        public string Url { get; set; }
        public bool Normalized { get; set; }
        public bool Header { get; set; }
        public string Separator { get; set; }
    }
}
