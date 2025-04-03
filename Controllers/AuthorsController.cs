using BookStoreAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.Models.DTOs.Author;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {

        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult<IEnumerable<ReadAuthorDTO>>> GetAuthors([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                var result = await _authorService.GetAuthorsAsync(skip, take);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult<ReadAuthorDTO>> GetAuthorById(int id)
        {
            try
            {
                var result = await _authorService.GetAuthorByIdAsync(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] ReadAuthorDTO authorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _authorService.UpdateAuthorAsync(id, authorDto);
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
        public async Task<ActionResult> CreateAuthor([FromBody] ReadAuthorDTO authorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _authorService.CreateAuthorAsync(authorDto);
                if (result == null) return Conflict();
                return CreatedAtAction(nameof(GetAuthorById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            try
            {
                var result = await _authorService.DeleteAuthorAsync(id);
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
