﻿using Microsoft.EntityFrameworkCore;
using StockManagement_DTO.Global;
using StockManagement_Persistance.Context;
using StockManagement_Persistance.Entities;

namespace StockManagement_Metier.Services
{
    public class StorageService : IStorageService
    {
        private readonly StockManagementContext _context;

        public StorageService(StockManagementContext context)
        {
            _context = context;
        }

        public async Task<List<StorageListingDTO>> FindAll()
        {
            return await _context.Storages
                .Select(s => new StorageListingDTO()
                {
                    Id = s.Id,
                    Name = s.Name,
                    HasProducts = s.Products != null ? s.Products.Count > 0 : false,
                })
                .ToListAsync();
        }

        public async Task<StorageListingDTO?> FindOneById(int id)
        {
            return await _context.Storages
                .Select(s => new StorageListingDTO()
                {
                    Id = s.Id,
                    Name = s.Name,
                    HasProducts = s.Products != null ? s.Products.Count > 0 : false,
                })
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<StorageListingDTO?> FindOneByName(string name)
        {
            return await _context.Storages
                .Where(w => w.Name == name)
                .Select(s => new StorageListingDTO()
                {
                    Id = s.Id,
                    Name = s.Name,
                    HasProducts = s.Products != null ? s.Products.Count > 0 : false,
                })
                .FirstOrDefaultAsync();
        }

        public async Task Insert(StorageCreationDTO viewModel)
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
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (storage != null)
            {
                _context.Storages.Remove(storage);

                await _context.SaveChangesAsync();
            }
        }
    }
}
