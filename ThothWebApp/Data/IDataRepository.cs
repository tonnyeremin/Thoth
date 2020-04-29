using System.Collections.Generic;

namespace Thoth.Data
{
    public interface IDataRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(long i);
        void Add(T entity);
        void Update(T entity, T newEntity);
        void Delete(T entity);
    }
}