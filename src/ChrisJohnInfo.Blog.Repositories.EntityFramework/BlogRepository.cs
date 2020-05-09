using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Repositories.EntityFramework
{
    public class BlogRepository : IBlogRepository
    {
        public Task<IEnumerable<PostViewModel>> GetPosts()
        {
            throw new System.NotImplementedException();
        }
    }
}
