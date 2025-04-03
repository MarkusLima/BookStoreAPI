using BookStoreAPI.Interface;
using BookStoreAPI.Models.DTOs.User;
using BookStoreAPI.Tools;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(Name= "login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO userDto)
        {

            try
            {
                var result = await _userService.LoginUserAsync(userDto);
                return Ok(new { token = result });
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
