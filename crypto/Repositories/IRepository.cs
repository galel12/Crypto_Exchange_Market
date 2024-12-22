using System.Collections.Generic;

namespace crypto.Repositories
{
    public interface IRepository<T>
    {
        T Save(T entity);
        T Get (T entity);
        T Update(T entity);
        bool Delete(int id);
        IEnumerable<T> GetAll();
    }
}