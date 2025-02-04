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

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}