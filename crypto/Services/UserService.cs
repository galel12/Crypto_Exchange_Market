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

namespace crypto.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repo, IConfiguration config)
        {
            _userRepository = repo;
            _configuration= config;
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

        public User Update(int id, User updatedUser)
        {
            var existingUser = _userRepository.GetById(id);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found");

            // Update user details
            existingUser.Username = updatedUser.Username ?? existingUser.Username;

            updatedUser.HashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(updatedUser.HashPassword, 13);
            existingUser.HashPassword = updatedUser.HashPassword ?? existingUser.HashPassword;

            // Save changes
            return _userRepository.Update(existingUser);
        }

        public bool Delete(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            // Delete the user
            return _userRepository.Delete(id);
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public async Task<(User user, SecurityToken token)> GetUserByLoginAsync(string username, string password)
        {
            // Validate user credentials (using ValidateUserAsync)
            var user = await ValidateUserAsync(username, password);

            // Generate the token
            var token = createToken(user.Username);

            // Return user and token
            return (user, token);
        }

        public User GetUserByToken(SecurityToken token)
        {
            throw new NotImplementedException();
        }

         // Validate the password during login
        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null )
            {
                throw new Exception("User not found");
            }
            if(!BCrypt.Net.BCrypt.EnhancedVerify(password, user.HashPassword))
            {
                throw new Exception("Incorrect password");
            }

            return user; // Authentication successful
        }

        private SecurityToken createToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secret = jwtSettings["Secret"];
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException("JWT Secret is not configured.");
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

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _userRepository.GetUserByUsernameAsync(username);
        }
    }
}