using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Interface;
using BookStoreAPI.Models.DTOs.Book;
using BookStoreAPI.Models.DTOs.User;
using BookStoreAPI.Models.Entities;
using BookStoreAPI.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadUserDTO>> GetUsersAsync(int skip, int take)
        {

            IQueryable<User> query = _context.Users.Include(r => r.Role);
            var results = await query.Skip(skip).Take(take).ToListAsync();
            return _mapper.Map<List<ReadUserDTO>>(results);

        }

        public async Task<ReadUserDTO> GetUserByIdAsync(int id)
        {
            var result = await _context.Users
                .Include(r => r.Role)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (result == null) throw new ExceptionsCode("User not found", 404);

            return _mapper.Map<ReadUserDTO>(result);
        }

        public async Task<ReadRoleNameAndUserIdDTO> GetRoleUserByEmailAsync(string email)
        {
            var result = await _context.Users
                .Include(r => r.Role)
                .FirstOrDefaultAsync(b => b.email == email);

            if (result == null) throw new ExceptionsCode("Book not found", 404);

            if (result.Role?.name == null) throw new ExceptionsCode("Role not found", 400);

            var roleNameAndUserId = new ReadRoleNameAndUserIdDTO
            {
                roleName = result.Role.name,
                userId = result.Id
            };

            return roleNameAndUserId;
        }

        public async Task<bool> UpdateUserAsync(int id, WriteUserDTO userDto)
        {
            var result = await _context.Users.FindAsync(id);
            if (result == null) throw new ExceptionsCode("User not found", 404);

            var existingUser = await _context.Users.FirstOrDefaultAsync(c => c.email == userDto.email);
            if (existingUser != null) throw new ExceptionsCode("User alread exist", 404); ;

            _mapper.Map(userDto, result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReadUserDTO> CreateUserAsync(WriteUserDTO userDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(c => c.email == userDto.email);
            if (existingUser != null) throw new ExceptionsCode("User alread exist", 404);

            var user = _mapper.Map<User>(userDto);

            var passwordHasher = new PasswordHasher<User>();
            user.password = passwordHasher.HashPassword(user, userDto.password);
            user.createdAt = DateTime.Now;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadUserDTO>(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var result = await _context.Users.FindAsync(id);
            if (result == null) throw new ExceptionsCode("User not found", 404);

            _context.Users.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> LoginUserAsync(LoginUserDTO userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.email == userDto.email);
            if (user == null) throw new ExceptionsCode("Invalid credentials", 404);

            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.password, userDto.password);
            if (result == PasswordVerificationResult.Failed) throw new ExceptionsCode("Invalid credentials", 404);

            // Instancia TokenService
            var tokenService = new TokenService();
            return tokenService.GenerateToken(user);

        }
    }
}
