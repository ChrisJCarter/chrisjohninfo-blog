using System;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChrisJohnInfo.Blog.AdminUI.Pages.Posts
{
    public class CreateModel : PageModel
    {
        private readonly IAdminService _service;

        public CreateModel(IAdminService service)
        {
            _service = service;
        }

        public IEnumerable<SelectListItem> Authors { get; set; }

        public async Task OnGetAsync()
        {
            Authors = (await _service.GetAuthorsAsync())
                .OrderByDescending(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .Select(a => new SelectListItem($"{a.LastName}, {a.FirstName}", a.AuthorId.ToString()));
        }

        public async Task<IActionResult> OnPostAsync(Post post)
        {
            bool.TryParse(Request.Form["IsPublished"][0], out var isPublished);
            post.DatePublished = isPublished ? (DateTime?)DateTime.Now : null;
            await _service.CreatePostAsync(post);
            return RedirectToPage("/Posts/Index");
        }
    }
}