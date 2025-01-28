using StockManagement_Persistance.Entities;

namespace StockManagement_Metier.ConsoleServices
{
    public interface IConsoleProductService
    {
        Task<List<Product>> GatherUnderMinimalQuantityProduct();
    }
}
