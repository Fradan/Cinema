using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public interface ICinemaService
    {
        Task<Cinema> GetByIdAsync(int id);

        Task<List<Cinema>> GetAllAsync();

        Task DeleteAsync(int id);
    }
}
