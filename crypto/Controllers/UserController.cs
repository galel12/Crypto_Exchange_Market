using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crypto.Models;
using crypto.Services;
using crypto.Dtos;
using crypto.Queries;
using Microsoft.AspNetCore.Authorization;

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
                var createdUserDto = await _userService.CreateUserAsync(newUserDto);
                return CreatedAtAction(nameof(GetUserByIdAsync), new { id = createdUserDto.Id }, createdUserDto);
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
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid id)
        {
           try{
                var userDto = await _userService.GetUserByIdAsync(id);
                return Ok(userDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { error = $"User with ID {id} not found." });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "An error occurred while fetching the user." });
            }

        }
        
        [Authorize]
        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, [FromBody] UpdateUserDto updatedUser)
        {
            try
            {
                var UpdatedUserDto = await _userService.UpdateAsync(id, updatedUser);
                
                return Ok(UpdatedUserDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest(new { error = "An error occurred while updating the user." });
            }
        }

        [Authorize]
        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
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