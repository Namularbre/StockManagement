using Microsoft.EntityFrameworkCore;
using StockManagement_Persistance.Context;
using StockManagement_Persistance.Entities;

namespace StockManagement_Metier.Services
{
    public class ConsoleProductService : IConsoleProductService
    {
        private readonly StockManagementContext _context;

        public ConsoleProductService(StockManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GatherUnderMinimalQuantityProduct()
        {
            return await _context.Products
                .Where(w => w.Quantity < w.MinQuantity)
                .ToListAsync();
        }
    }
}
