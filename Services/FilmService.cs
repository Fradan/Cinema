using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<List<Film>> GetAllAsync()
        {
            return await _filmRepository.GetAllAsync();
        }
    }
}
