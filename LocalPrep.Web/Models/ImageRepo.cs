using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LocalPrep.Web.Models
{
    public static class ImageRepo
    {
        public static string UploadImage(HttpPostedFileBase file, string location)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=localprepassets;AccountKey=YQKXs0/ZHzBjKkNmG2LedCHxw9tm2LZwVCEm/30hr2OE6GNWo7c30bpaXrqtA29JdVUsd6r1zZ0CQ5m4525qdA==;EndpointSuffix=core.windows.net");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("lpstorage");

            CloudBlockBlob blob = container.GetBlockBlobReference(location + "/" + file.FileName);
            blob.UploadFromStream(file.InputStream);

            return blob.Uri.ToString();
        }

        public static void RemoveImage(string fileLocation)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=localprepassets;AccountKey=YQKXs0/ZHzBjKkNmG2LedCHxw9tm2LZwVCEm/30hr2OE6GNWo7c30bpaXrqtA29JdVUsd6r1zZ0CQ5m4525qdA==;EndpointSuffix=core.windows.net");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("lpstorage");

            CloudBlockBlob oldBlob = container.GetBlockBlobReference(fileLocation);

            if (oldBlob.Exists())
            {
                oldBlob.DeleteIfExists();
            }
        }
    }
}