﻿using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.Repositories.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

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

            return await query.Select(p =>
                          new PostViewModel
                          {
                              Author = p.Author.NickName,
                              Title = p.Title,
                              Content = p.Content,
                              DatePublished = p.DatePublished.Value
                          }).ToListAsync();
        }
    }
}
