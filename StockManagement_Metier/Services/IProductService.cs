﻿using StockManagement_DTO.Global;
using StockManagement_Persistance.Entities;

namespace StockManagement_Metier.Services
{
    public interface IProductService
    {
        Task<List<ProductListingDTO>> FindAllByStorage(int storageId);
        Task<Product?> FindOneById(int id);
        bool ProductAsAlerts(Product product);
        Task Insert(ProductCreationDTO model);
        Task DeleteById(int id);
    }
}
