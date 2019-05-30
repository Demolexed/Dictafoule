using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace TextToTask.WebJob
{
    class Program
    {
        static void Main()
        {
            var config = new JobHostConfiguration();
            config.Queues.BatchSize = 1;
            config.Queues.MaxDequeueCount = 1;
            var host = new JobHost(config);
            host.RunAndBlock();
        }
    }
}
