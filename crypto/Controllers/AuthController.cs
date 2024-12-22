using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using crypto.Services;
using crypto.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService service)
    {
        _userService = service;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            var loginResult = _userService.GetUserByLogin(request.Username, request.Password);

            var tokenHandler = new JwtSecurityTokenHandler();
            return Ok(new { Token = tokenHandler.WriteToken(loginResult.token), User = loginResult.user });
        }
        catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
    }
}