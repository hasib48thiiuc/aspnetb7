using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Demo.Models;

namespace Web.Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseModel _coursemod;

        public HomeController(ILogger<HomeController> logger,ICourseModel coursemod)
        {
            _logger = logger;
            _coursemod = coursemod;
        }

        public IActionResult Index()
        {
            _logger.LogInformation(" i am in index");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}