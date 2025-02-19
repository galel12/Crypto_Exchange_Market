using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using crypto.Dtos;
using crypto.Models;
using crypto.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace crypto.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }
        public SecurityToken GenerateToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException("JWT_SECRET_KEY environment variable is missing.");
            }
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.CreateToken(tokenDescriptor);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var secret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException("JWT_SECRET_KEY environment variable is missing.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ValidAudience = _configuration["JwtSettings:Audience"]
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
                return principal;
            }
            catch
            {
                return null; // Token is invalid
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<TokenResponseDto> ValidateAndSaveRefreshTokenAsync(RefreshTokenRequestDto request)
        {   
            try
            {
                var user = await validateRefreshTokenAsync(request.Id, request.RefreshToken);

                // Generate new tokens
                var newAccessToken = GenerateToken(user);
                var newRefreshToken = GenerateRefreshToken();

                await saveRefreshTokenAsync(user, newRefreshToken); // Save to database

                var response = new TokenResponseDto
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    RefreshToken = newRefreshToken
                };

                return response;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task<User?> validateRefreshTokenAsync(Guid id ,string refreshToken)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user is null || user.RefreshTokenExpiryTime <= DateTime.UtcNow || user.RefreshToken != refreshToken)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");
            }
            return user;
        }

        public async Task saveRefreshTokenAsync(User user, string newRefreshToken)
        {
            // Update user's refresh token and expiry time
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30);
            await _userRepository.UpdateAsync(user);
        }
    }
}