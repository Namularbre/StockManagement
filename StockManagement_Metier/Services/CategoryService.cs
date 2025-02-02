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
                    HasProducts = s.Products != null && s.Products.Count > 0,
                })
                .ToListAsync();
        }

        public async Task<Category?> FindOneById(int idCategory)
        {
            return await _context.Categories
                .Where(w => w.Id == idCategory)
                .Include(i => i.Products)
                .FirstOrDefaultAsync();
        }

        public async Task<Category?> FindOneByName(string name)
        {
            return await _context.Categories
                .Where(w => w.Name == name)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task Insert(CategoryCreationDTO dto)
        {
            var category = new Category()
            {
                Name = dto.Name
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Category MUST exists in database
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task Update(CategoryUpdateDTO dto)
        {
            var category = await _context.Categories
                .Where(w => w.Id ==  dto.Id)
                .FirstAsync();

            category.Name = dto.Name;
            category.Id = dto.Id;

            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }
    }
}
