using System.Collections.Generic;
using crypto.Queries;

namespace crypto.Repositories
{
    public interface IRepository<T>
    {
        Task<T> SaveAsync(T entity);
        T Get(T entity);
        Task<T> UpdateAsync(T entity);
        bool Delete(int id);
        Task<IEnumerable<T>> GetAllAsync(QueryObject query);
    }
}