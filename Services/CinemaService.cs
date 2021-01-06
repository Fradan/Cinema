using Application.Exceptions;
using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public class CinemaService : ICinemaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CinemaService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task DeleteAsync(int id)
        {
            var cinema = await _unitOfWork.Cinemas.FindByIdAsync(id);
            if (cinema == null)
            {
                throw new BusinessRuleValidationException($"Объекта с идентификатором {id} не существует.");
            }
            _unitOfWork.Cinemas.Delete(cinema);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Cinema>> GetAllAsync()
        {
            return await _unitOfWork.Cinemas.GetAllAsync();
        }

        public async Task<Cinema> GetByIdAsync(int id)
        {
            var res = await _unitOfWork.Cinemas.FindByIdAsync(id);
            return res;
        }
    }
}
