using Microsoft.AspNetCore.Mvc;
using StockManagement_DTO.Global;
using StockManagement_Metier.Services;

namespace StockManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
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
            ViewBag.StorageName = await _productService.GetProductStorageName(idStorage);

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> New(int idStorage)
        {
            ViewBag.IdStorage = idStorage;
            ViewBag.Categories = await _categoryService.FindAll();
            ViewBag.StorageName = await _productService.GetProductStorageName(idStorage);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(ProductCreationDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.Insert(model);

                    return RedirectToAction("Index", new { idStorage = model.IdStorage });
                }
                catch (Exception)
                {
                    // Category is not found (The user CHANGED the id in the input to make it false
                    return Unauthorized("You must give a category that exists");
                }
            }

            ViewBag.IdStorage = model.IdStorage;
            ViewBag.Categories = await _categoryService.FindAll();
            ViewBag.StorageName = await _productService.GetProductStorageName(model.IdStorage);

            return View(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int idProduct, int idStorage)
        {
            var product = await _productService.FindOneById(idProduct);

            if (product != null)
            {
                if (!_productService.ProductHasAlerts(product))
                {
                    await _productService.DeleteById(idProduct);

                    return new OkResult();
                }
                
                return Unauthorized("Vous ne pouvez pas supprimer un produit contenu dans des alertes");
            }
            return NotFound("Produit introuvable");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int idProduct)
        {
            var product = await _productService.FindOneById(idProduct);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _categoryService.FindAll();

            var dto = new ProductUpdateDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                IdStorage = product.Storage.Id,
                Quantity = product.Quantity,
                MinQuantity = product.MinQuantity,
                IdCategory = product.Category.Id
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDTO product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProduct(product);

                return RedirectToAction("Index", new { idStorage = product.IdStorage });
            }

            return View(product);
        }

        [HttpPut]
        public async Task<IActionResult> RemoveOneProductQuantity(int idProduct)
        {
            var product = await _productService.FindOneById(idProduct);

            if (product == null)
            {
                return NotFound("Produit introuvable");
            }
            else if (product.Quantity <= 0)
            {
                return Unauthorized("Il n'y a déjà plus de produit");
            }
            else
            {
                var dto = new ProductUpdateDTO()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    IdStorage = product.Storage.Id,
                    Quantity = product.Quantity - 1,
                    MinQuantity = product.MinQuantity,

                };

                await _productService.UpdateProduct(dto);

                return new OkResult();
            }
        }
    }
}
