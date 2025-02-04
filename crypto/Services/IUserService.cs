using crypto.Dtos;
using crypto.Models;
using crypto.Queries;
using Microsoft.IdentityModel.Tokens;

namespace crypto.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(NewUserDto newUserDto);
        Task<User> UpdateAsync(int id, NewUserDto updatedUser);
        Task<(User user, SecurityToken token)> GetUserByLoginAsync(string username, string password);
        User GetUserByToken(SecurityToken token);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync(QueryObject query);
        Task<User> GetUserByIdAsync(int id);
        Task<User?> ValidateUserAsync(string username, string password);
    }
}