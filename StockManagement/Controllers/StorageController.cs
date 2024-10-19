using Microsoft.AspNetCore.Mvc;
using StockManagement.Models;
using StockManagement.Services;

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
            List<StorageViewModel> storages = await _storageService.FindAll(); 

            return View(storages);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(StorageViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _storageService.Insert(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
