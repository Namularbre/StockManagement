using StockManagement_Metier.Services;
using Microsoft.Extensions.Logging;
using StockManagement_ConsoleApp.Utils;

namespace StockManagement_ConsoleApp.App
{
    internal class App : IApp
    {
        private readonly IConsoleAlertService _alertService;
        private readonly IConsoleProductService _productService;
        private readonly ILogger<App> _logger;

        private EConsoleAppError errorCode;

        public App(IConsoleAlertService alertService, IConsoleProductService productService, ILogger<App> logger)
        {
            _alertService = alertService;
            _productService = productService;
            _logger = logger;

            errorCode = EConsoleAppError.None;
        }

        public async Task Run()
        {
            _logger.LogInformation("Running StockManagement_ConsoleApp");

            var product = new List<StockManagement_Persistance.Entities.Product>();

            try
            {
                product = await _productService.GatherUnderMinimalQuantityProduct();
                _logger.LogInformation($"Gathered {product.Count} product(s)");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while gathering products : {ex.Message}");
                errorCode = EConsoleAppError.ProductGathering;
            }

            try
            {
                await _alertService.GenerateAlerts(product);
                _logger.LogInformation("Alerts generated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while generating alerts : {ex.Message}");
                errorCode = EConsoleAppError.AlertGeneration;
            }

            try
            {
                await _alertService.PurgeOldAlerts();
                _logger.LogInformation("Purged old alerts");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while purging old alerts : {ex.Message}");
                errorCode = EConsoleAppError.PurgeOldAlerts;
            }

            _logger.LogInformation($"Finished with error code {errorCode}");
        }
    }
}
