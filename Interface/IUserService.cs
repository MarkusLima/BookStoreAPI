using BookStoreAPI.Models.DTOs.User;

namespace BookStoreAPI.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<ReadUserDTO>> GetUsersAsync(int skip, int take);
        Task<ReadUserDTO> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, WriteUserDTO userDto);
        Task<ReadUserDTO> CreateUserAsync(WriteUserDTO userDto);
        Task<bool> DeleteUserAsync(int id);
        Task<string> LoginUserAsync(LoginUserDTO userDto);
    }
}
