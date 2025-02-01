using Microsoft.EntityFrameworkCore;
using StockManagement_DTO.Global;
using StockManagement_Persistance.Entities;
using StockManagement_Persistance.Context;

namespace StockManagement_Metier.Services
{
    public class ProductService : IProductService
    {
        private readonly StockManagementContext _context;

        public ProductService(StockManagementContext context)
        {
            _context = context;
        }

        public async Task<List<ProductListingDTO>> FindAllByStorage(int storageId)
        {
            return await _context.Products
                .Where(w => w.Storage.Id == storageId)
                .Include(i => i.Category)
                .Select(s => new ProductListingDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Quantity = s.Quantity,
                    MinQuantity = s.MinQuantity,
                    StorageName = s.Storage.Name,
                    Description = s.Description,
                    IdStorage = storageId,
                    IsEssential = s.IsEssential,
                    CategoryName = s.Category.Name,
                })
                .ToListAsync();
        }

        public async Task<Product?> FindOneById(int id)
        {
            return await _context.Products
                .Where(w => w.Id == id)
                .Include(i => i.Storage)
                .Include(i => i.Category)
                .FirstOrDefaultAsync();
        }

        public bool ProductHasAlerts(Product product) => product.Alerts != null && product.Alerts.Count > 0;

        public async Task Insert(ProductCreationDTO model)
        {
            var storage = _context.Storages
                .Where(w => w.Id == model.IdStorage)
                .FirstOrDefault();

            if (storage == null)
            {
                return;
            }

            var category = await _context.Categories
                .Where(w => w.Id == model.IdCategory)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                throw new Exception($"Category not found, id: {model.IdCategory}");
            }

            var product = new Product
            {
                Name = model.Name,
                Quantity = model.Quantity,
                MinQuantity = model.MinQuantity != null ? (int)model.MinQuantity : 0,
                Storage = storage,
                Description = model.Description,
                IsEssential = model.IsEssential,
                Category = category
            };

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var product = await _context.Products
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<string?> GetProductStorageName(int idStorage)
        {
            return await _context.Storages
                .Where(w => w.Id == idStorage)
                .Select(s => s.Name)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto">MUST contains a VALID ID</param>
        /// <returns></returns>
        public async Task UpdateProduct(ProductUpdateDTO dto)
        {
            var product = await _context.Products
                .Where(w => w.Id == dto.Id)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return;
            }

            var category = await _context.Categories
                .Where(w => w.Id == dto.IdCategory)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return;
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Quantity = dto.Quantity;
            product.MinQuantity = dto.MinQuantity != null ? (int) dto.MinQuantity : 0;
            product.IsEssential = dto.IsEssential;
            product.UpdatedAt = DateTime.Now;
            product.Category = category;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
