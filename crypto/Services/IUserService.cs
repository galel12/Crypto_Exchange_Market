using crypto.Models;
using Microsoft.IdentityModel.Tokens;

namespace crypto.Services
{
    public interface IUserService
    {
        User CreateUser(User user);
        User Update(int id, User updatedUser);
        (User user, SecurityToken token) GetUserByLogin(string username, string password);
        User GetUserByToken(SecurityToken token);
        bool Delete(int id);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
    }
}