using BookStoreAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.Models.DTOs.Category;

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
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult<ReadCategoryDTO>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null) return NotFound();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] ReadCategoryDTO categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _categoryService.UpdateCategoryAsync(id, categoryDto);
                if (!result) return Conflict();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        //[RoleMiddleware("admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateCategory([FromBody] ReadCategoryDTO categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var category = await _categoryService.CreateCategoryAsync(categoryDto);
                if (category == null) return Conflict();
                return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}