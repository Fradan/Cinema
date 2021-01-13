using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public interface IFilmService
    {
        Task<List<Film>> GetAllAsync();
    }
}
