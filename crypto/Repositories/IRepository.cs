using System.Collections.Generic;
using crypto.Queries;

namespace crypto.Repositories
{
    public interface IRepository<T>
    {
        Task<T> SaveAsync(T entity);
        Task<T> GetAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(QueryObject query);
    }
}