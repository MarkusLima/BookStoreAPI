using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Interface;
using BookStoreAPI.Models.DTOs.Book;
using BookStoreAPI.Models.Entities;
using BookStoreAPI.Tools;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadBookDTO>> GetBooksAsync(int skip, int take, string keyword = "")
        {
           
            IQueryable<Book> query = _context.Books.Include(a => a.Author).Include(c => c.Category);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(b => b.title.Contains(keyword) || b.Author.name.Contains(keyword) || b.Category.name.Contains(keyword));
            }

            var results = await query.Skip(skip).Take(take).ToListAsync();
            return _mapper.Map<List<ReadBookDTO>>(results);

        }

        public async Task<ReadBookDTO> GetBookByIdAsync(int id)
        {
            var result = await _context.Books
                .Include(a => a.Author)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (result == null) throw new ExceptionsCode("Book not found", 404);

            return _mapper.Map<ReadBookDTO>(result);
        }

        public async Task<bool> UpdateBookAsync(int id, WriteBookDTO bookDto)
        {
            var result = await _context.Books.FindAsync(id);
            if (result == null) throw new ExceptionsCode("Book not found", 404);

            var existingBook = await _context.Books.FirstOrDefaultAsync(c => c.title == bookDto.title);
            if (existingBook != null) throw new ExceptionsCode("Book alread exist", 400);

            _mapper.Map(bookDto, result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReadBookDTO> CreateBookAsync(WriteBookDTO bookDto)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(c => c.title == bookDto.title);
            if (existingBook != null) throw new ExceptionsCode("Book alread exist", 400);

            var book = _mapper.Map<Book>(bookDto);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadBookDTO>(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var result = await _context.Books.FindAsync(id);
            if (result == null) throw new ExceptionsCode("Book not found", 404); ;

            _context.Books.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
