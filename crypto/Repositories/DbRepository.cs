using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto.Repositories;
using crypto.Models;
using crypto.Data;
using Microsoft.EntityFrameworkCore;
using crypto.Queries;

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

        public async Task<T> SaveAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetAsync(T entity)
        {
            var foundEntity = await _dbSet.FindAsync(entity);
            if (foundEntity == null)
                throw new InvalidOperationException("Entity not found.");
            return foundEntity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync(QueryObject query)
        {
            var users = _dbSet.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(query.Username))
            {
                users = users.Where(u => EF.Property<string>(u, "Username").Contains(query.Username));
            }

            // Sorting
            if(!string.IsNullOrEmpty(query.SortBy))
            {
                if(query.SortBy.Equals("Username", StringComparison.OrdinalIgnoreCase))
                {
                    users = query.IsAscending ? users.OrderBy(u => EF.Property<string>(u, "Username")) : users.OrderByDescending(u => EF.Property<string>(u, "Username"));
                }
                if(query.SortBy.Equals("DateCreated", StringComparison.OrdinalIgnoreCase))
                {
                    users = query.IsAscending ? users.OrderBy(u => EF.Property<DateTime>(u, "DateCreated")) : users.OrderByDescending(u => EF.Property<DateTime>(u, "DateCreated"));
                }
            }

            // Pagination
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await users.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }
    }
}