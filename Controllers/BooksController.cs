using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.Interface;
using BookStoreAPI.Models.DTOs.Book;
using BookStoreAPI.Tools;
using BookStoreAPI.Midlewares;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {

        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadBookDTO>>> GetBooks([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string keyword = "")
        {
            try
            {
                var result = await _bookService.GetBooksAsync(skip, take, keyword);
                return Ok(result);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadBookDTO>> GetBookById(int id)
        {
            try
            {
                var result = await _bookService.GetBookByIdAsync(id);
                return Ok(result);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }


        [HttpPut("{id}")]
        [RoleMiddleware("Adm")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] WriteBookDTO bookDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _bookService.UpdateBookAsync(id, bookDto);
                return NoContent();
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost]
        [RoleMiddleware("Adm")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateBook([FromBody] WriteBookDTO bookDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _bookService.CreateBookAsync(bookDto);
                return CreatedAtAction(nameof(GetBookById), new { id = result.Id }, result);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [RoleMiddleware("Adm")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                var result = await _bookService.DeleteBookAsync(id);
                return NoContent();
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

    }
}
