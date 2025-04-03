using BookStoreAPI.Models.DTOs.Category;

namespace BookStoreAPI.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<ReadCategoryDTO>> GetCategoriesAsync(int skip, int take);
        Task<ReadCategoryDTO> GetCategoryByIdAsync(int id);
        Task<bool> UpdateCategoryAsync(int id, ReadCategoryDTO categoryDto);
        Task<ReadCategoryDTO> CreateCategoryAsync(ReadCategoryDTO categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
