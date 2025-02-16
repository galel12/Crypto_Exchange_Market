using crypto.Dtos;
using crypto.Models;
using crypto.Queries;
using Microsoft.IdentityModel.Tokens;

namespace crypto.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(NewUserDto newUserDto);
        Task<UserResponseDto> UpdateAsync(int id, UpdateUserDto updatedUser);
        Task<(User user, SecurityToken token)> GetUserByLoginAsync(string username, string password);
        Task<User> GetUserByTokenAsync(SecurityToken token);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(QueryObject query);
        Task<UserResponseDto> GetUserByIdAsync(int id);
        Task<User?> ValidateUserAsync(string username, string password);
    }
}