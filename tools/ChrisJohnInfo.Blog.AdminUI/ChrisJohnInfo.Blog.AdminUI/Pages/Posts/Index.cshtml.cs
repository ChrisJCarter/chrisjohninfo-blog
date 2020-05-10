using System.Collections.Generic;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChrisJohnInfo.Blog.AdminUI.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly IAdminService _service;

        public IndexModel(IAdminService service)
        {
            _service = service;
        }

        public IEnumerable<Post> Posts { get; set; } = new List<Post>();

        public async Task OnGetAsync()
        {
            Posts = await _service.GetPostsAsync();
        }
    }
}