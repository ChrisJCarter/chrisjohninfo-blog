using System;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Contracts.Interfaces
{
    public interface IStaticResourceHandler
    {
        Task<(byte[] content, string contentType)> GetAsync(Guid key, string resourceName);
    }
}
