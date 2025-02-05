using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto.Repositories;
using crypto.Models;
using crypto.Queries;

namespace crypto.Repositories
{
    public class InMemoryRep : IUserRepository
    {
        private readonly Dictionary<int, User> _mockDb = new Dictionary<int, User>();
        private int _nextId = -1;

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(User entity)
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

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return _mockDb.FirstOrDefault((pair) => pair.Value.Username == username).Value;
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

        public Task<IEnumerable<User>> GetAllAsync(QueryObject queryObject)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(int id)
        {
            return Task.FromResult(_mockDb.TryGetValue(id, out var user) ? user : null);
        }
        
        public Task<User> UpdateAsync(User entity)
        {
            if (_mockDb.ContainsKey(entity.Id))
            {
                _mockDb[entity.Id] = entity;
                return Task.FromResult(entity);
            }
            else
            {
                throw new KeyNotFoundException($"User with Id {entity.Id} not found.");
            }
        }
    }
}