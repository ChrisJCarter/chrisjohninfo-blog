using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChrisJohnInfo.Blog.AdminUI.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly IAdminService _service;

        public EditModel(IAdminService service)
        {
            _service = service;
        }

        public Author Author { get; set; }

        public async Task OnGetAsync(int id)
        {
            Author = await _service.GetAuthorAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(Author author)
        {
            await _service.UpdateAuthorAsync(author);
            return RedirectToPage("/Authors/Index");
        }
    }
}