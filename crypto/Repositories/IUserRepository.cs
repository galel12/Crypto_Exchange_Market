using System.Collections.Generic;
using crypto.Models;

namespace crypto.Repositories
{
     public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByIdAsync(Guid id); // Ensure this method is here
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
    }
}