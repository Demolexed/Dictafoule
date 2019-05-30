using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DictaFoule.Common.Tools
{
    public static class AzureBlobStorage
    {
        private static CloudBlobContainer GetBlobContainer(string containerName, bool createIfNotExists = true)
        {
            var azureCustomer085 = ConfigurationManager.ConnectionStrings["AzureDictaFoule"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(azureCustomer085);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            if (createIfNotExists)
            {
                container.CreateIfNotExists();
            }
            return container;
        }

        public static string Upload(HttpPostedFileBase file, string fileName, string Container)
        {
            var container = GetBlobContainer(Container);

            var blob = container.GetBlockBlobReference(fileName);
            blob.Properties.ContentType = file.ContentType;
            blob.UploadFromStream(file.InputStream);

            return blob.Uri.AbsoluteUri;
        }

        public static string Upload(Stream fileStream, string contentType, string fileName, string Container)
        {
            var container = GetBlobContainer(Container);

            var blob = container.GetBlockBlobReference(fileName);
            blob.Properties.ContentType = contentType;
            blob.UploadFromStream(fileStream);

            return blob.Uri.AbsoluteUri;
        }

        public static string Get(string name, string folder)
        {
            var container = GetBlobContainer(folder);
            var blob = container.GetBlockBlobReference(name);
            var sharedAccessPolicy = new SharedAccessBlobPolicy
            {
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-10),
                SharedAccessExpiryTime = DateTime.UtcNow.AddDays(30),
                Permissions = SharedAccessBlobPermissions.Read,
            };
            var sharedAccessSignature = blob.GetSharedAccessSignature(sharedAccessPolicy);
            return blob.Uri.AbsoluteUri + sharedAccessSignature;
        }

        public static Stream GetStream(string name, string folder)
        {
            var container = GetBlobContainer(folder);
            var blob = container.GetBlockBlobReference(name);
            using (var memoryStream = new MemoryStream())
            {
                blob.DownloadToStream(memoryStream);
                Stream stream = new MemoryStream();
                memoryStream.WriteTo(stream);
                stream.Position = 0;
                return stream;

            }
        }
    }
}