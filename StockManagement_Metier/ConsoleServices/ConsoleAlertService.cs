using Microsoft.EntityFrameworkCore;
using StockManagement_Persistance.Context;
using StockManagement_Persistance.Entities;

namespace StockManagement_Metier.ConsoleServices
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
            if (products.Count != 0)
            {
                var alert = new Alert()
                {
                    Products = products,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Treated = false,
                };

                _context.Alerts.Add(alert);

                await _context.SaveChangesAsync();
            }
        }

        public async Task PurgeOldAlerts()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var oldAlerts = await _context.Alerts
                        .Where(w => w.UpdatedAt < DateTime.Now.AddDays(-30))
                        .ToListAsync();

                    _context.Alerts.RemoveRange(oldAlerts);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();

                    throw;
                }
            }
        }
    }
}
