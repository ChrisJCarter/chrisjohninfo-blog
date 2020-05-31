using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.MvcUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;

namespace ChrisJohnInfo.Blog.MvcUI.Controllers
{
    public class PostsController : Controller
    {
        private readonly IBlogService _service;
        
        public PostsController(IBlogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _service.GetPosts();
            return View(posts);
        }

        [Authorize]
        public async Task<IActionResult> Preview(Guid id)
        {
            var post = await _service.GetPost(id);
            return View(post);
        }

        [HttpGet("posts/{postId}/static/{resource}")]
        public IActionResult Static(Guid postId, string resource)
        {
            var localResourcePath = @$"D:\uploads\{postId}\{resource}";
            var content = System.IO.File.ReadAllBytes(localResourcePath);
            return File(content, GetMimeType(resource));
        }
        private static string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
