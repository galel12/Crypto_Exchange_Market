using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crypto.Models;
using crypto.Services;
using crypto.Dtos;
using crypto.Queries;

namespace crypto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] NewUserDto newUserDto)
       {
            try
            {
                var createdUser = await _userService.CreateUserAsync(newUserDto);
                return CreatedAtAction(nameof(GetUserByIdAsync), new { id = createdUser.Id }, createdUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500,  "An error occurred while creating the user." );
            }
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { error = $"User with ID {id} not found." });
            }
            return Ok(user);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] NewUserDto updatedUser)
        {
            try
            {
                var user = await _userService.UpdateAsync(id, updatedUser);
                if (user == null)
                {
                    return NotFound(new { error = $"User with ID {id} not found." });
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest(new { error = "An error occurred while updating the user." });
            }
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
            {
                var isDeleted = await _userService.DeleteAsync(id);
                if (!isDeleted)
                {
                    return NotFound(new { error = $"User with ID {id} not found." });
                }
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest(new { error = "An error occurred while deleting the user." });
            }
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] QueryObject query)
        {
            var users = await _userService.GetAllUsersAsync(query);
            return Ok(users);
        }
    }
}