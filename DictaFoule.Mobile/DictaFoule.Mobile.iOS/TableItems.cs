using System;
using Foundation;

namespace DictaFoule.Mobile.iOS
{
    public class TableItem
    {
        public string Name { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }
        public int Transcribed { get; set; }
        public NSUrl Pathfile { get; set; }
        public TableItem() { }

        public TableItem(string heading)
        { 
            Name = heading;
            Transcribed = 0;
        }

    }
}