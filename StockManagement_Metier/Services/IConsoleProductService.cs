using StockManagement_Persistance.Entities;

namespace StockManagement_Metier.Services
{
    public interface IConsoleProductService
    {
        Task<List<Product>> GatherUnderMinimalQuantityProduct();
    }
}
