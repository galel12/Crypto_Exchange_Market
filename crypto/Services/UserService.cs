using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using crypto.Models;
using crypto.Repositories;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using crypto.Dtos;
using crypto.Queries;

namespace crypto.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repo, IConfiguration config)
        {
            _userRepository = repo;
            _configuration = config;
        }

        public async Task<User> CreateUserAsync(NewUserDto newUserDto)
        {
            // Validate user input
            if (string.IsNullOrEmpty(newUserDto.Username))
                throw new ArgumentException("Username is required");

            if (string.IsNullOrEmpty(newUserDto.Password))
                throw new ArgumentException("Password is required");

            // Hash the password with 13 salt rounds (work factor)
            var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(newUserDto.Password, 13);

            // Check if the username already exists
            var existingUser = await _userRepository.GetUserByUsernameAsync(newUserDto.Username);
            if (existingUser != null)
                throw new InvalidOperationException("Username already exists");

            // Create the user object
            var user = new User
            {
                Username = newUserDto.Username,
                HashPassword = hashedPassword,
                DateCreated = DateTime.UtcNow // Automatically set CreatedAt
            };

            // Save the user
            return await _userRepository.SaveAsync(user);
        }

        public async Task<User> UpdateAsync(int id, NewUserDto updatedUser)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found");

            // Update user details
            existingUser.Username = updatedUser.Username ?? existingUser.Username;

            // Check if the password has changed
            if (!string.IsNullOrEmpty(updatedUser.Password) &&
                !BCrypt.Net.BCrypt.EnhancedVerify(updatedUser.Password, existingUser.HashPassword))
            {
                existingUser.HashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(updatedUser.Password, 13);
            }

            // Save changes
            return await _userRepository.UpdateAsync(existingUser);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            // Delete the user
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(QueryObject query)
        {
            return await _userRepository.GetAllAsync(query);
        }

        public async Task<(User user, SecurityToken token)> GetUserByLoginAsync(string username, string password)
        {
            // Validate user credentials (using ValidateUserAsync)
            var user = await ValidateUserAsync(username, password);

            // Generate the token
            if (string.IsNullOrEmpty(user?.Username))
            {
                throw new ArgumentNullException(nameof(user.Username), "Username cannot be null or empty.");
            }
            var token = createToken(user.Username);

            // Return user and token
            return (user, token);
        }

        public async Task<User> GetUserByTokenAsync(SecurityToken token)
        {
            throw new NotImplementedException();
        }

        // Validate the password during login
        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (!BCrypt.Net.BCrypt.EnhancedVerify(password, user.HashPassword))
            {
                throw new Exception("Incorrect password");
            }

            return user; // Authentication successful
        }

        private SecurityToken createToken(string username)
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
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.CreateToken(tokenDescriptor);
        }

        public Task<User?> GetUserByUsernameAsync(string username)
        {
            return _userRepository.GetUserByUsernameAsync(username);
        }
    }
}