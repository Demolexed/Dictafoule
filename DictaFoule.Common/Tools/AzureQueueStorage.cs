using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Configuration;

namespace DictaFoule.Common.Tools
{
    public static class AzureQueueStorage
    {
        private static CloudQueueClient GetQueueClient()
        {
            var azureWebJobsStorage = ConfigurationManager.ConnectionStrings["AzureDictaFoule"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(azureWebJobsStorage);
            var queueClient = storageAccount.CreateCloudQueueClient();
            return queueClient;
        }

        public static void QueueProject(int Id, string QueueName)
        {
            var queueClient = GetQueueClient();
            var writingQueue = queueClient.GetQueueReference(QueueName);
            writingQueue.CreateIfNotExists();
            var message = new CloudQueueMessage(Id.ToString());
            writingQueue.AddMessage(message);
        }
    }
}
