using Microsoft.AspNetCore.Mvc;
using StockManagement_Metier.Services;

namespace StockManagement.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.FindAll();

            return View(categories);
        }
    }
}
