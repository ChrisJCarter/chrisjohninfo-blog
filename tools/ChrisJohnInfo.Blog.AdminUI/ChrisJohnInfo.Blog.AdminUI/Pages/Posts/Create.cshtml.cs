using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            await _service.CreatePostAsync(post);
            return RedirectToPage("/Posts/Index");
        }
    }
}