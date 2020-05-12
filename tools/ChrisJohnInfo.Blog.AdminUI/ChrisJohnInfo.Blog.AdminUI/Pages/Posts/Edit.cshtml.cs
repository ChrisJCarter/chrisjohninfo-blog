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
    public class EditModel : PageModel
    {
        public Post Post { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }
        private readonly IAdminService _service;

        public EditModel(IAdminService service)
        {
            _service = service;
        }

        public async Task OnGetAsync(Guid id)
        {
            Post = await _service.GetPostAsync(id);
            Authors = (await _service.GetAuthorsAsync())
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .Select(a => new SelectListItem($"{a.LastName}, {a.FirstName}", a.AuthorId.ToString(), Post.AuthorId == a.AuthorId));
        }

        public async Task<IActionResult> OnPostAsync(Post post)
        {
            await _service.UpdatePostAsync(post);
            return RedirectToPage("/Posts/Index");
        }
    }
}