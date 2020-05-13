using ChrisJohnInfo.Blog.MvcUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ChrisJohnInfo.Blog.MvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogService _service;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IBlogService service, IConfiguration configuration)
        {
            _logger = logger;
            _service = service;
            _configuration = configuration;
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
