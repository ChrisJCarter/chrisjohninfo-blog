using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using ChrisJohnInfo.Blog.MvcUI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.MvcUI.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IAdminService _service;

        public PostsController(IAdminService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _service.GetPostsAsync();
            return View(posts);
        }

        public async Task<IActionResult> Create() => await Edit(Guid.Empty);

        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            await _service.CreatePostAsync(post);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var viewModel = new PostViewModel();
            viewModel.Post = id == Guid.Empty ? new Post() : await _service.GetPostAsync(id);
            viewModel.Authors = (await _service.GetAuthorsAsync())
                .Select(a => new SelectListItem($"{a.LastName}, {a.FirstName}", a.AuthorId.ToString()));
            return View("Edit", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            await _service.UpdatePostAsync(post);
            if (string.Equals(Request.Form["updateAction"], "updateOnly", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var post = await _service.GetPostAsync(id);
            return View(post);
        }

        public async Task<IActionResult> Delete(Post post)
        {
            await _service.DeletePostAsync(post.PostId);
            return RedirectToAction(nameof(Index));
        }
    }
}
