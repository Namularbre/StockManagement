using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Entities;
using StockManagement.Models;

namespace StockManagement.Services
{
    public interface IStorageService
    {
        Task<List<StorageViewModel>> FindAll();
        Task<StorageViewModel?> FindOneById(int id);
        Task Insert(StorageViewModel viewModel);
        Task Delete(int id);
    }

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
                })
                .ToListAsync();
        }

        public async Task<StorageViewModel?> FindOneById(int id)
        {
            return await _context.Storages
                .Select (s => new StorageViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Insert(StorageViewModel viewModel)
        {
            var storage = new Storage()
            {
                Id = viewModel.Id,
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
                _context.Remove(id);

                await _context.SaveChangesAsync();
            }
        }
    }
}
