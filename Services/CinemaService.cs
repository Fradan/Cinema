using Application.Exceptions;
using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;

        public CinemaService(ICinemaRepository cinemaRepository) => _cinemaRepository = cinemaRepository;

        public async Task DeleteAsync(int id)
        {
            var cinema = await _cinemaRepository.FindByIdAsync(id);
            if (cinema == null)
            {
                throw new BusinessRuleValidationException($"Объекта с идентификатором {id} не существует.");
            }
            _cinemaRepository.Delete(cinema);
        }

        public async Task<List<Cinema>> GetAllAsync()
        {
            return await _cinemaRepository.GetAllAsync();
        }

        public async Task<Cinema> GetByIdAsync(int id)
        {
            var res = await _cinemaRepository.FindByIdAsync(id);
            return res;
        }
    }
}
