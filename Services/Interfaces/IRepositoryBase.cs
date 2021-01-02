using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public interface IRepositoryBase<T> where T : class
    {
        Task AddAsync(T entity);

        T Update(T entity);

        Task<List<T>> GetAllAsync();

        void Delete(T entity);

        Task<T> FindByIdAsync(int id);
    }
}
