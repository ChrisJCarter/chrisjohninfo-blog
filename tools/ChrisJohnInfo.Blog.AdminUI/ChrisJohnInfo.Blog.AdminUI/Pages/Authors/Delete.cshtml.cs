using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace ChrisJohnInfo.Blog.AdminUI.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly IAdminService _service;

        public DeleteModel(IAdminService service)
        {
            _service = service;
        }
        public Author Author { get; set; }
        public async Task OnGetAsync(int id)
        {
            Author = await _service.GetAuthorAsync(id);
            
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _service.DeleteAuthorAsync(id);
            return RedirectToPage("/Authors/Index");
        }
    }
}