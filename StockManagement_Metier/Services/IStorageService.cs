using StockManagement_DTO.Global;

namespace StockManagement_Metier.Services
{
    public interface IStorageService
    {
        Task<List<StorageListingDTO>> FindAll();
        Task<StorageListingDTO?> FindOneById(int id);
        Task<StorageListingDTO?> FindOneByName(string name);
        Task Insert(StorageCreationDTO viewModel);
        Task Delete(int id);
    }
}
