using ChrisJohnInfo.Blog.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Contracts.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Post>> GetPosts();
    }
}
