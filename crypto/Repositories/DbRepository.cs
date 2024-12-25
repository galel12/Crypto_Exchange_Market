using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto.Repositories;
using crypto.Models;
using crypto.Data;
using Microsoft.EntityFrameworkCore;

namespace crypto.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public DbRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Save(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Get(T entity)
        {
            return _dbSet.Find(entity);
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}