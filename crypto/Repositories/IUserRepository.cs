using System.Collections.Generic;
using crypto.Models;

namespace crypto.Repositories
{
     public interface IUserRepository : IRepository<User>
    {
        User GetUserByUsername(string username);
        User GetById(int id); // Ensure this method is here
    }
}