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

    [HttpPost]
   public async Task<IActionResult> Login([FromBody] LoginRequest request)
{
    try
    {
        // Validate user credentials asynchronously
        var loginResult = await _userService.GetUserByLoginAsync(request.Username, request.Password);

        // Generate the JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(loginResult.token);

        // Return the token and user details
        return Ok(new { Token = tokenString, User = loginResult.user });
    }
    catch (Exception)
    {
        // Handle errors (e.g., invalid credentials)
        return Unauthorized("Invalid credentials.");
    }
}
}