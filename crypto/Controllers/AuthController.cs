using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using crypto.Services;
using crypto.Models;
using crypto.Dtos;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IUserService service, IAuthService authService)
    {
        _userService = service;
        _authService = authService;
    }

    [HttpPost]
    public async Task<ActionResult<TokenResponseDto>> LoginAsync([FromBody] LoginRequest request)
    {
        try
        {
            var loginResult = await _userService.GetUserByLoginAsync(request.Username, request.Password);

            // Generate the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(loginResult.token);

            // Generate and store refresh token
            var refreshToken = _authService.GenerateRefreshToken();
            await _authService.saveRefreshTokenAsync(loginResult.user, refreshToken); 

            var response = new TokenResponseDto { AccessToken = tokenString, RefreshToken = refreshToken };

            // Return the token and refresh token
            return Ok(response);
        }
        catch (Exception)
        {
            // Handle errors (e.g., invalid credentials)
            return Unauthorized("Invalid credentials.");
        }
    }

    // Refresh token endpoint
    [HttpPut]
    public async Task<ActionResult<TokenResponseDto>> RefreshTokenAsync([FromBody] RefreshTokenRequestDto request)
    {
        try
        {
            var tokenResponse = await _authService.ValidateAndSaveRefreshTokenAsync(request);
            return Ok(tokenResponse);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid or expired refresh token.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while refreshing the token.");
        }
    }


}