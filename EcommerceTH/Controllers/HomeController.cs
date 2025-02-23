using EcommerceTH.data;
using EcommerceTH.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceTH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceContext _db;

        public HomeController(ILogger<HomeController> logger, EcommerceContext db)
        {
            _logger = logger;
            _db = db;
        }


        public IActionResult Index()
        {
            var products = _db.Products.ToList();
            return View(products);
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
