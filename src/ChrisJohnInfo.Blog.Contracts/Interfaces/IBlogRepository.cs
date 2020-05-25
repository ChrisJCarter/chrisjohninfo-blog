﻿using ChrisJohnInfo.Blog.Contracts.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Contracts.Interfaces
{
    public interface IBlogRepository
    {
        Task<IEnumerable<PostViewModel>> GetPosts(bool publishedOnly);
    }
}
