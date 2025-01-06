using System.Collections.Generic;
using crypto.Models;

namespace crypto.Repositories
{
     public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByUsernameAsync(string username);
        User GetById(int id); // Ensure this method is here
    }
}