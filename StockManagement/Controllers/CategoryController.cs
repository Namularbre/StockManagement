using Microsoft.AspNetCore.Mvc;
using StockManagement_DTO.Global;
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

        [HttpGet]
        public async Task<IActionResult> New()
        {
            var dto = new CategoryCreationDTO();

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> New(CategoryCreationDTO dto)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryService.FindOneByName(dto.Name);

                if (category == null)
                {
                    await _categoryService.Insert(dto);

                    return RedirectToAction("Index");
                }

                return Unauthorized("La catégorie existe déjà");
            }

            return View(dto);
        }
    }
}
