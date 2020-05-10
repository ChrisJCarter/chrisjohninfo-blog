using ChrisJohnInfo.Blog.MvcUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.Contracts.Interfaces;

namespace ChrisJohnInfo.Blog.MvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogService _service;
        public HomeController(ILogger<HomeController> logger, IBlogService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetPosts());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
