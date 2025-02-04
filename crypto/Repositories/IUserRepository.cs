using System.Collections.Generic;
using crypto.Models;

namespace crypto.Repositories
{
     public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetByIdAsync(int id); // Ensure this method is here
    }
}