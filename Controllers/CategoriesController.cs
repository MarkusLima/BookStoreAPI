using BookStoreAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.Models.DTOs.Category;
using BookStoreAPI.Tools;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult<IEnumerable<ReadCategoryDTO>>> GetCategories([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                var categories = await _categoryService.GetCategoriesAsync(skip, take);
                return Ok(categories);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpGet("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult<ReadCategoryDTO>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(category);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPut("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] WriteCategoryDTO categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _categoryService.UpdateCategoryAsync(id, categoryDto);
                return NoContent();
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost]
        //[RoleMiddleware("admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateCategory([FromBody] WriteCategoryDTO categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var category = await _categoryService.CreateCategoryAsync(categoryDto);
                return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}