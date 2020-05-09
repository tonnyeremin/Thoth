using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThothBase
{
    public interface IDataRepository<T>
    {
        Task<List<T>> GetAll(QuoteItemParameters parameters);
        ValueTask<T> Get(long id);
        Task Add(T entity);
        Task Update(long id, T newEntity);
        Task Delete(T entity);
        Task<T> GetRandom();
    }
}