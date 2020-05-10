using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.AdminUI.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly IAdminService _service;

        public CreateModel(IAdminService service)
        {
            _service = service;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(Author author)
        {
            await _service.CreateAuthorAsync(author);
            return RedirectToPage("/Authors/Index");
        }
    }
}