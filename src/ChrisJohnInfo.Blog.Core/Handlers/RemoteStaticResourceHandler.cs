using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace ChrisJohnInfo.Blog.Core.Handlers
{
    public class RemoteStaticResourceHandler : StaticResourceHandler
    {
        private readonly string _storageConnectionString;

        public RemoteStaticResourceHandler(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
        }

        public override async Task<(byte[] content, string contentType)> GetAsync(Guid key, string resourceName)
        {
            var blobService = new BlobServiceClient(_storageConnectionString);

            var containerName = key.ToString();

            var containerClient = blobService.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(resourceName);

            await using var ms = new MemoryStream();
            var blob = await blobClient.DownloadToAsync(ms);
            return (ms.ToArray(), GetMimeType(resourceName));
        }

        
    }
}
