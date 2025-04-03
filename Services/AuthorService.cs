using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Interface;
using BookStoreAPI.Models.DTOs.Author;
using BookStoreAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuthorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadAuthorDTO>> GetAuthorsAsync(int skip, int take)
        {
            var authors = await _context.Authors.Skip(skip).Take(take).ToListAsync();
            return _mapper.Map<List<ReadAuthorDTO>>(authors);
        }

        public async Task<ReadAuthorDTO> GetAuthorByIdAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return null;
            return _mapper.Map<ReadAuthorDTO>(author);
        }

        public async Task<bool> UpdateAuthorAsync(int id, ReadAuthorDTO authorDto)
        {
            var findAuthor = await _context.Authors.FindAsync(id);
            if (findAuthor == null) return false;

            var existingAuthor = await _context.Authors.FirstOrDefaultAsync(c => c.name == authorDto.name);
            if (existingAuthor != null) return false;

            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            _mapper.Map(authorDto, author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReadAuthorDTO> CreateAuthorAsync(ReadAuthorDTO authorDto)
        {
            var existingAuthor = await _context.Authors.FirstOrDefaultAsync(c => c.name == authorDto.name);
            if (existingAuthor != null) return null;

            var author = _mapper.Map<Author>(authorDto);
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadAuthorDTO>(author);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
