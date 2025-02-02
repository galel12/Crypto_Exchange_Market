using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crypto.Models;
using crypto.Services;
using crypto.Dtos;

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
        public async Task<IActionResult> CreateUser([FromBody] NewUserDto newUserDto)
       {
            try
            {
                var createdUser = await _userService.CreateUserAsync(newUserDto);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
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
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { error = $"User with ID {id} not found." });
            }
            return Ok(user);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            try
            {
                var user = _userService.Update(id, updatedUser);
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
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var isDeleted = _userService.Delete(id);
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
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}