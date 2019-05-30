using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.WebJob
{
    class Answer
    {
        public Answer(int id, int cost)
        {
            Id = id;
            Cost = cost;
        }

        public int Id { get; private set; }
        public int Cost { get; private set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
