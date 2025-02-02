using StockManagement_DTO.Global;
using StockManagement_Persistance.Entities;

namespace StockManagement_Metier.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryListingDTO>> FindAll();
        Task<Category?> FindOneById(int idCategory);
        Task<Category?> FindOneByName(string name);
        Task Insert(CategoryCreationDTO dto);
        Task Update(CategoryUpdateDTO dto);
        Task Delete(Category category);
    }
}
