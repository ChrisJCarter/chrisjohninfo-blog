using ChrisJohnInfo.Blog.Contracts.Interfaces;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Core.Handlers
{
    public abstract class StaticResourceHandler : IStaticResourceHandler
    {
        public abstract Task<(byte[] content, string contentType)> GetAsync(Guid key, string resourceName);
        protected string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}