using BookStoreAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.Models.DTOs.Author;
using BookStoreAPI.Tools;

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
            var result = await _authorService.GetAuthorsAsync(skip, take);
            return Ok(result); 
        }

        [HttpGet("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult<ReadAuthorDTO>> GetAuthorById(int id)
        {
            try
            {
                var result = await _authorService.GetAuthorByIdAsync(id);
                return Ok(result);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPut("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] WriteAuthorDTO authorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _authorService.UpdateAuthorAsync(id, authorDto);
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
        public async Task<ActionResult> CreateAuthor([FromBody] WriteAuthorDTO authorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _authorService.CreateAuthorAsync(authorDto);
                return CreatedAtAction(nameof(GetAuthorById), new { id = result.Id }, result);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        //[RoleMiddleware("admin")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            try
            {
                var result = await _authorService.DeleteAuthorAsync(id);
                return NoContent();
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

    }

}
