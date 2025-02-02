using crypto.Dtos;
using crypto.Models;
using Microsoft.IdentityModel.Tokens;

namespace crypto.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(NewUserDto newUserDto);
        User Update(int id, User updatedUser);
        Task<(User user, SecurityToken token)> GetUserByLoginAsync(string username, string password);
        User GetUserByToken(SecurityToken token);
        Task<User> GetUserByUsernameAsync(string username);
        bool Delete(int id);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        Task<User?> ValidateUserAsync(string username, string password);
    }
}