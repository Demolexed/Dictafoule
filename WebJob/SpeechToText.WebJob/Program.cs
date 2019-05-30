using Microsoft.Azure.WebJobs;

namespace SpeechToText.WebJob
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
