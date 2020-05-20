using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using ChrisJohnInfo.Blog.Repositories.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Repositories.EntityFramework
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ChrisJohnInfoBlogContext _context;

        public BlogRepository(ChrisJohnInfoBlogContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPosts(bool publishedOnly)
        {
            var query = (from p in _context.Posts
                select p);

            if (publishedOnly)
            {
                query = query.Where(p => p.DatePublished.HasValue);
            }


            return await query.Select(p =>
                          new Post
                          {
                              PostId = p.PostId,
                              Title = p.Title,
                              Content = p.Content,
                              DatePublished = p.DatePublished,
                              AuthorId = p.AuthorId
                          }).ToListAsync();
        }
    }
}
