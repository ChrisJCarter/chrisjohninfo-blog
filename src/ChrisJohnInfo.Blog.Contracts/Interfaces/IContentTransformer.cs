using System;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Contracts.Interfaces
{
    public interface IContentTransformer
    {
        Task<string> Transform(Guid postId, string content);
    }
}
