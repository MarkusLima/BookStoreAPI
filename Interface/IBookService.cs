using BookStoreAPI.Models.DTOs.Book;

namespace BookStoreAPI.Interface
{
    public interface IBookService
    {
        Task<IEnumerable<ReadBookDTO>> GetBooksAsync(int skip, int take, string keyword);
        Task<ReadBookDTO> GetBookByIdAsync(int id);
        Task<bool> UpdateBookAsync(int id, WriteBookDTO bookDto);
        Task<ReadBookDTO> CreateBookAsync(WriteBookDTO bookDto);
        Task<bool> DeleteBookAsync(int id);
        
    }
}
