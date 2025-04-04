﻿using BookStoreAPI.Interface;
using BookStoreAPI.Midlewares;
using BookStoreAPI.Models.DTOs.User;
using BookStoreAPI.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        [RoleMiddleware("Adm")]
        public async Task<ActionResult<IEnumerable<ReadUserDTO>>> GetUsers([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                var result = await _userService.GetUsersAsync(skip, take);
                return Ok(result);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

        }

        [HttpGet("{id}")]
        [Authorize]
        [RoleMiddleware("Adm")]
        public async Task<ActionResult<ReadUserDTO>> GetUserById(int id)
        {
            try
            {
                var result = await _userService.GetUserByIdAsync(id);
                return Ok(result);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        [RoleMiddleware("Adm")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] WriteUserDTO userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _userService.UpdateUserAsync(id, userDto);
                return NoContent();
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateUser([FromBody] WriteUserDTO userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _userService.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize]
        [RoleMiddleware("Adm")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (ExceptionsCode ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

    }
}
