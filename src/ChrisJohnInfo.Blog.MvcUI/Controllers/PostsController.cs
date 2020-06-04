using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.MvcUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.MvcUI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Memory;

namespace ChrisJohnInfo.Blog.MvcUI.Controllers
{
    public class PostsController : Controller
    {
        private readonly IBlogService _service;
        private readonly IStaticResourceHandler _staticResourceHandler;
        private readonly IMemoryCache _cache;

        public PostsController(IBlogService service, IStaticResourceHandler staticResourceHandler, IMemoryCache cache)
        {
            _service = service;
            _staticResourceHandler = staticResourceHandler;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            if (!_cache.TryGetValue<IEnumerable<Contracts.ViewModels.PostViewModel>>("posts", out var posts))
            {
                posts = await _service.GetPosts();
                _cache.Set("posts", posts, TimeSpan.FromMinutes(5));
            }
            return View(posts);
        }

        [Authorize]
        public async Task<IActionResult> Preview(Guid id)
        {
            var post = await _service.GetPost(id);
            return View(post);
        }

        [HttpGet("posts/{postId}/static/{resource}")]
        public async Task<IActionResult> Static(Guid postId, string resource)
        {
            var (content, mimeType) = await _staticResourceHandler.GetAsync(postId, resource);

            return File(content, mimeType);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
