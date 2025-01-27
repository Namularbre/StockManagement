using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Entities;
using StockManagement.Models;

namespace StockManagement.Services
{
    public class StorageService : IStorageService
    {
        private readonly StockManagementContext _context;

        public StorageService(StockManagementContext context)
        {
            _context = context;
        }
        
        public async Task<List<StorageViewModel>> FindAll()
        {
            return await _context.Storages
                .Select(s => new StorageViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    HasProducts = s.Products != null ? s.Products.Count > 0 : false,
                })
                .ToListAsync();
        }

        public async Task<StorageViewModel?> FindOneById(int id)
        {
            return await _context.Storages
                .Select(s => new StorageViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    HasProducts = s.Products != null ? s.Products.Count > 0 : false,
                })
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<StorageViewModel?> FindOneByName(string name)
        {
            return await _context.Storages
                .Where(w => w.Name == name)
                .Select(s => new StorageViewModel()
                {
                    Id= s.Id,
                    Name = s.Name,
                    HasProducts = s.Products != null ? s.Products.Count > 0 : false,
                })
                .FirstOrDefaultAsync();
        }

        public async Task Insert(CreateStorageViewModel viewModel)
        {
            var storage = new Storage()
            {
                Id = 0,
                Name = viewModel.Name,
            };

            await _context.AddAsync(storage);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var storage = await _context.Storages
                .Where (w => w.Id == id)
                .FirstOrDefaultAsync();

            if (storage != null)
            {
                _context.Storages.Remove(storage);

                await _context.SaveChangesAsync();
            }
        }
    }
}
