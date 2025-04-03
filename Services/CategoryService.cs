using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Interface;
using BookStoreAPI.Models.DTOs.Category;
using BookStoreAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadCategoryDTO>> GetCategoriesAsync(int skip, int take)
        {
            var categories = await _context.Categories.Skip(skip).Take(take).ToListAsync();
            return _mapper.Map<List<ReadCategoryDTO>>(categories);
        }

        public async Task<ReadCategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;
            return _mapper.Map<ReadCategoryDTO>(category);
        }

        public async Task<bool> UpdateCategoryAsync(int id, ReadCategoryDTO categoryDto)
        {
            var findCategory = await _context.Categories.FindAsync(id);
            if (findCategory == null) return false;

            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.name == categoryDto.name);
            if (existingCategory != null) return false;

            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _mapper.Map(categoryDto, category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReadCategoryDTO> CreateCategoryAsync(ReadCategoryDTO categoryDto)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.name == categoryDto.name);
            if (existingCategory != null) return null;

            var category = _mapper.Map<Category>(categoryDto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadCategoryDTO>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
