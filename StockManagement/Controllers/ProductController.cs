using Microsoft.AspNetCore.Mvc;
using StockManagement_DTO.Global;
using StockManagement_Metier.Services;

namespace StockManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Gather all the products of a storage
        /// </summary>
        /// <param name="idStorage"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(int idStorage)
        {
            var products = await _productService.FindAllByStorage(idStorage);

            ViewBag.IdStorage = idStorage;

            return View(products);
        }

        [HttpGet]
        public IActionResult New(int idStorage)
        {
            ViewBag.IdStorage = idStorage;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(ProductCreationDTO model)
        {
            if (ModelState.IsValid)
            {
                await _productService.Insert(model);

                return RedirectToAction("Index", new { idStorage = model.IdStorage });
            }

            ViewBag.IdStorage = model.IdStorage;

            return View(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int idProduct, int idStorage)
        {
            var product = await _productService.FindOneById(idProduct);

            if (product != null)
            {
                if (!_productService.ProductAsAlerts(product))
                {
                    await _productService.DeleteById(idProduct);

                    return new OkResult();
                }
                
                return Unauthorized("Vous ne pouvez pas supprimer un produit contenu dans des alertes");
            }
            return NotFound("Produit introuvable");
        }
    }
}
