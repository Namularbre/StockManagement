using Microsoft.AspNetCore.Mvc;
using StockManagement_DTO.Global;
using StockManagement_Metier.Services;

namespace StockManagement.Controllers
{
    public class StorageController : Controller
    {
        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService) {
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var storages = await _storageService.FindAll();

            return View(storages);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(StorageCreationDTO model)
        {
            if (!ModelState.IsValid) return View(model);

            // TODO : add error message
            if (await _storageService.FindOneByName(model.Name) != null)
            {
                return View(model);
            }

            await _storageService.Insert(model);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var storage = await _storageService.FindOneById(id);

            if (storage != null)
            {
                if (storage.HasProducts)
                {
                    return Unauthorized("Vous ne pouvez pas supprimer un stockage avec des produits");
                }

                await _storageService.Delete(id);

                return new OkResult();
            }
            return NotFound();
        }
    }
}
