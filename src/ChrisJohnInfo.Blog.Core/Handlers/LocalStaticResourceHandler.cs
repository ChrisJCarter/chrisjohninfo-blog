using System;
using System.IO;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Core.Handlers
{
    public class LocalStaticResourceHandler : StaticResourceHandler
    {
        private readonly string _contentDirectory;

        public LocalStaticResourceHandler(string contentDirectory)
        {
            _contentDirectory = contentDirectory;
        }

        public override Task<(byte[] content, string contentType)> GetAsync(Guid key, string resourceName)
        {
            var resourcePath = Path.Combine(_contentDirectory, key.ToString(), resourceName);
            return Task.FromResult((File.ReadAllBytes(resourcePath), GetMimeType(resourceName)));
        }
    }
}
