using Microsoft.EntityFrameworkCore;
using StockManagement_DTO.Global;
using StockManagement_Persistance.Context;
using StockManagement_Persistance.Entities;

namespace StockManagement_Metier.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly StockManagementContext _context;

        public CategoryService(StockManagementContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryListingDTO>> FindAll()
        {
            return await _context.Categories
                .Select(s => new CategoryListingDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .ToListAsync();
        }

        public async Task<Category?> FindOneById(int idCategory)
        {
            return await _context.Categories
                .Where(w => w.Id == idCategory)
                .FirstOrDefaultAsync();
        }
    }
}
