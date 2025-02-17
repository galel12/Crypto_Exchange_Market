using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace crypto.Services
{
    public interface IAuthService
    {
        SecurityToken GenerateToken(string username, string role);
        ClaimsPrincipal? ValidateToken(string token);
        string GenerateRefreshToken();
        Task<string> RefreshTokenAsync(string currentRefreshToken, IUserService userService);
    }
}