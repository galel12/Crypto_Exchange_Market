using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto.Data;
using crypto.Models;
using Microsoft.EntityFrameworkCore;

namespace crypto.Repositories
{
    public class UserRepository : DbRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }

        public User? GetById(int id)
        {
            return _dbSet.FirstOrDefault(u => u.Id == id);
        }
    }
}