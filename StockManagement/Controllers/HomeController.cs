using Microsoft.AspNetCore.Mvc;
using StockManagement.Models;
using StockManagement_Metier.Services;
using System.Diagnostics;

namespace StockManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Products = await _productService.FindRecentlyUpdatedOnes();
            ViewBag.ProductInAlertCount = await _productService.FindAmountOfProductInLastAlert();

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
