using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using crypto.Dtos;
using crypto.Models;

namespace crypto.Services
{
    public interface IAuthService
    {
        SecurityToken GenerateToken(string username, string role);
        ClaimsPrincipal? ValidateToken(string token);
        string GenerateRefreshToken();
        Task<TokenResponseDto> ValidateAndSaveRefreshTokenAsync(RefreshTokenRequestDto request);
        Task saveRefreshTokenAsync(User user, string newRefreshToken);
    }
}