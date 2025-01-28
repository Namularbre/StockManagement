using StockManagement_Persistance.Entities;

namespace StockManagement_Metier.Services
{
    public interface IConsoleAlertService
    {
        Task GenerateAlerts(List<Product> products);
    }
}
