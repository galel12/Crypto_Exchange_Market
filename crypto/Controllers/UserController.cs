using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crypto.Models;
using crypto.Services;

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

        [HttpPost("signUp")]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                var newUser = _userService.CreateUser(user);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}