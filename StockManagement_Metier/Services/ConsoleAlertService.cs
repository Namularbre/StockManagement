using StockManagement_Persistance.Context;
using StockManagement_Persistance.Entities;

namespace StockManagement_Metier.Services
{
    public class ConsoleAlertService : IConsoleAlertService
    {
        private readonly StockManagementContext _context;

        public ConsoleAlertService(StockManagementContext context)
        {
            _context = context;
        }

        public async Task GenerateAlerts(List<Product> products)
        {
            var alert = new Alert()
            {
                Products = products,
                Author = null,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Treated = false,
            };

            _context.Alerts.Add(alert);

            await _context.SaveChangesAsync();
        }
    }
}
