using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Interface;
using BookStoreAPI.Models.DTOs.Category;
using BookStoreAPI.Models.Entities;
using BookStoreAPI.Tools;
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
            if (category == null) throw new ExceptionsCode("Category not found", 404); ;
            return _mapper.Map<ReadCategoryDTO>(category);
        }

        public async Task<bool> UpdateCategoryAsync(int id, WriteCategoryDTO categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new ExceptionsCode("Category not found", 404); ;

            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.name == categoryDto.name && c.Id == id);
            if (existingCategory != null) throw new ExceptionsCode("Category alread exist", 400); ;

            _mapper.Map(categoryDto, category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReadCategoryDTO> CreateCategoryAsync(WriteCategoryDTO categoryDto)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.name == categoryDto.name);
            if (existingCategory != null) throw new ExceptionsCode("Category alread exist", 400); ;

            var category = _mapper.Map<Category>(categoryDto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadCategoryDTO>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new ExceptionsCode("Category not found", 404); ;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
