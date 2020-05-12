using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChrisJohnInfo.Blog.AdminUI.Pages.Posts
{
    public class DeleteModel : PageModel
    {
        public readonly IAdminService _service;

        public DeleteModel(IAdminService service)
        {
            _service = service;
        }

        public Post Post { get; set; }
        public async Task OnGetAsync(Guid id)
        {
            Post = await _service.GetPostAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            await _service.DeletePostAsync(id);
            return RedirectToPage("/Posts/Index");
        }
    }
}