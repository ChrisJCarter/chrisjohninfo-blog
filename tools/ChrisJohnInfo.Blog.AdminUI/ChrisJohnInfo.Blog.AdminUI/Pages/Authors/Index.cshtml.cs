using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.AdminUI.Pages.Authors
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Author> Authors { get; set; } = new List<Author>();
        private readonly IAdminService _service;

        public IndexModel(IAdminService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            Authors = await _service.GetAuthorsAsync();
        }
    }
}