using Microsoft.AspNetCore.Mvc;
using StockManagement_Metier.Services;

namespace StockManagement.Controllers
{
    public class AlertController : Controller
    {
        private readonly IAlertService _alertService;

        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        public async Task<IActionResult> Index()
        {
            var alerts = await _alertService.FindAllNotFinished();

            return View(alerts);
        }
    }
}
