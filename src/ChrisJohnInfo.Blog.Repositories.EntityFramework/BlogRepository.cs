using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.ViewModels;
using ChrisJohnInfo.Blog.Repositories.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<IEnumerable<PostViewModel>> GetPosts(bool publishedOnly)
        {
            var query = (from p in _context.Posts
                select p);

            if (publishedOnly)
            {
                query = query.Where(p => p.DatePublished.HasValue);
            }

            query.Include(p => p.Author);

            return await query.Select(p =>
                          new PostViewModel
                          {
                              PostId = p.PostId,
                              Title = p.Title,
                              Content = p.Content,
                              DatePublished = p.DatePublished,
                              AuthorName = p.Author.NickName ?? $"{p.Author.FirstName} {p.Author.LastName}"
                          }).ToListAsync();
        }

        public async Task<PostViewModel> GetPost(Guid postId)
        {
            var entity = await _context.Posts.Include(p => p.Author).FirstOrDefaultAsync(p => p.PostId == postId);
            if (entity == null)
            {
                return null;
            }

            return new PostViewModel
            {
                PostId = entity.PostId,
                Title = entity.Title,
                Content = entity.Content,
                DatePublished = entity.DatePublished,
                AuthorName = entity.Author.NickName ?? $"{entity.Author.FirstName} {entity.Author.LastName}"
            };
        }
    }
}
