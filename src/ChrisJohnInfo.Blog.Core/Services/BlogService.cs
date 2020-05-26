﻿using System;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IEnumerable<PostViewModel>> GetPosts()
        {
            return await _blogRepository.GetPosts(publishedOnly: true);
        }

        public async Task<PostViewModel> GetPost(Guid postId)
        {
            return await _blogRepository.GetPost(postId);
        }
    }
}
