using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.WebJob
{
    class CreateJobResponse
    {

        public CreateJobResponse(int jobId, int cost, int balance)
        {
            Job = new Answer(jobId, cost);
        }

        public Answer Job { get; private set; }

        public int Balance { get; private set; }
    }
}
