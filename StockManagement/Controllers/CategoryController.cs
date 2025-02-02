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
        public IActionResult New()
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

        [HttpGet]
        public async Task<IActionResult> Update(int idCategory)
        {
            var category = await _categoryService.FindOneById(idCategory);

            if (category == null)
            {
                return Unauthorized("La catégorie n'existe pas");
            }

            var dto = new CategoryUpdateDTO()
            {
                Id = idCategory,
                Name = category.Name,
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDTO dto)
        {
            var category = await _categoryService.FindOneById(dto.Id);

            if (category != null)
            {
                await _categoryService.Update(dto);
                return RedirectToAction("Index");
            }
            else
            {
                return View(dto);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int idCategory)
        {
            var category = await _categoryService.FindOneById(idCategory);

            if (category != null)
            {
                if (category.Products == null || category.Products != null && category.Products.Count == 0)
                {
                    await _categoryService.Delete(category);
                }

                return Unauthorized("Vous ne pouvez pas supprimer une catégorie qui contient des produits");
            }
            
            return NotFound("Catégorie introuvable");
        }
    }
}
