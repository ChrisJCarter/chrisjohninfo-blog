using System.Security.Cryptography;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChrisJohnInfo.Blog.MvcUI.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class AuthorsController : Controller
    {
        private readonly IAdminService _service;

        public AuthorsController(IAdminService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var authors = await _service.GetAuthorsAsync();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Edit", new Author());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            await _service.CreateAuthorAsync(author);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _service.GetAuthorAsync(id);
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author author)
        {
            await _service.UpdateAuthorAsync(author);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _service.GetAuthorAsync(id);
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Author author)
        {
            await _service.DeleteAuthorAsync(author.AuthorId);
            return RedirectToAction(nameof(Index));
        }
    }
}
