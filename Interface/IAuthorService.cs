using BookStoreAPI.Models.DTOs.Author;

namespace BookStoreAPI.Interface
{
    public interface IAuthorService
    {
        Task<IEnumerable<ReadAuthorDTO>> GetAuthorsAsync(int skip, int take);
        Task<ReadAuthorDTO> GetAuthorByIdAsync(int id);
        Task<bool> UpdateAuthorAsync(int id, ReadAuthorDTO authorDto);
        Task<ReadAuthorDTO> CreateAuthorAsync(ReadAuthorDTO authorDto);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
