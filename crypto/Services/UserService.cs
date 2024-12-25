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

        public User CreateUser(User user)
        {
          // Validate user input
            if (string.IsNullOrEmpty(user.Username))
                throw new ArgumentException("Username is required");

            if (string.IsNullOrEmpty(user.HashPassword))
                throw new ArgumentException("Password is required");

            // Check if the username already exists
            if (_userRepository.GetUserByUsername(user.Username) != null)
                throw new InvalidOperationException("Username already exists");

            // Save the user
            return _userRepository.Save(user);
        }

        public User Update(int id, User updatedUser)
        {
            var existingUser = _userRepository.GetById(id);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found");

            // Update user details
            existingUser.Username = updatedUser.Username ?? existingUser.Username;
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

        public (User user, SecurityToken token) GetUserByLogin(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);

            if(user == null)
            {
                throw new Exception("User not found");
            }

            if (user.HashPassword != password)
            {
                throw new Exception("Incorrect password");
            }

            return (user, createToken(user.Username));
        }

        public User GetUserByToken(SecurityToken token)
        {
            throw new NotImplementedException();
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
    }
}