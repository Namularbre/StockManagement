using Microsoft.EntityFrameworkCore;
using StockManagement_Persistance.Context;
using StockManagement_Persistance.Entities;
using StockManagement_DTO.Global;

namespace StockManagement_Metier.Services
{
    public class AlertService : IAlertService
    {
        private readonly StockManagementContext _context;

        public AlertService(StockManagementContext context)
        {
            _context = context;
        }

        public async Task<List<AlertListingDTO>> FindAllNotFinished()
        {
            return await _context.Alerts
                .OrderBy(a => a.CreatedAt)
                .Where(w => w.Treated == false)
                .Select(s => new AlertListingDTO()
                {
                    Products = s.Products,
                    Author = s.Author,
                    Treated = s.Treated,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt,
                })
                .ToListAsync();
        }

    }
}
