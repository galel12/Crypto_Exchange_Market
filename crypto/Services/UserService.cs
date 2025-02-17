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
using crypto.Mappers;

namespace crypto.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository repo, IConfiguration config, IAuthService authService)
        {
            _userRepository = repo;
            _authService = authService;
        }

        public async Task<UserResponseDto> CreateUserAsync(NewUserDto newUserDto)
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
            var savedUser = await _userRepository.SaveAsync(user);

            return UserMappers.MapToUserResponseDto(savedUser);
        }

        public async Task<UserResponseDto> UpdateAsync(int id, UpdateUserDto updatedUser)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
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
            var UpdatedUser = await _userRepository.UpdateAsync(existingUser);

            return UserMappers.MapToUserResponseDto(UpdatedUser);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            // Delete the user
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<UserResponseDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            return UserMappers.MapToUserResponseDto(user);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(QueryObject query)
        {
            var users = await _userRepository.GetAllAsync(query);
            // Map each User to a UserResponseDto
            var userDtos = users.Select(user => UserMappers.MapToUserResponseDto(user));

            return userDtos;
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
            var token = _authService.GenerateToken(user.Username, "User");

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

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime expiryTime)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = expiryTime;

            await _userRepository.UpdateAsync(user);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _userRepository.GetUserByRefreshTokenAsync(refreshToken);
        }
    }
}