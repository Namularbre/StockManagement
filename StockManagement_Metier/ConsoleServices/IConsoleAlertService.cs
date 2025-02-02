using StockManagement_Persistance.Entities;

namespace StockManagement_Metier.ConsoleServices
{
    public interface IConsoleAlertService
    {
        Task GenerateAlerts(List<Product> products);
        Task PurgeOldAlerts();
    }
}
