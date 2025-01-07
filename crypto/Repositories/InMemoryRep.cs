using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto.Repositories;
using crypto.Models;

namespace crypto.Repositories
{
    public class InMemoryRep : IUserRepository
    {
        private readonly Dictionary<int, User> _mockDb = new Dictionary<int, User>();
        private int _nextId = -1;

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User? GetUserByUsername(string username)
        {
            return _mockDb.FirstOrDefault((pair) => pair.Value.Username == username).Value;
        }

        public Task<User?> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public User Save(User entity)
        {
            //critical section
            int id = ++_nextId;

            User user = new User{
                Id = id,
                Name = entity.Name,
                Username = entity.Username,
                HashPassword = entity.HashPassword
           
            };

            _mockDb[id] = user;

            System.Console.WriteLine($"user: {user}\n has created");

            return user;
        }

        public Task<User> SaveAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}