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
                .Select(s => new ProductListingDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Quantity = s.Quantity,
                    MinQuantity = s.MinQuantity,
                    StorageName = s.Storage.Name,
                    Description = s.Description,
                    IdStorage = s.IdStorage,
                })
                .ToListAsync();
        }

        public async Task<Product?> FindOneById(int id)
        {
            return await _context.Products
                .Where(w => w.Id == id)
                .Include(i => i.Storage)
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

            var product = new Product
            {
                Name = model.Name,
                Quantity = model.Quantity,
                MinQuantity = model.MinQuantity != null ? (int)model.MinQuantity : 0,
                Storage = storage,
                Description = model.Description,
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
        /// <param name="dto">MUST contain a VALID ID</param>
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

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Quantity = dto.Quantity;
            product.MinQuantity = dto.MinQuantity != null ? (int) dto.MinQuantity : 0;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
