using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public class CinemaService : ICinemaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CinemaService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<Cinema>> GetAllAsync()
        {
            return await _unitOfWork.Cinemas.GetAllAsync() ?? new List<Cinema>(0);
        }

        public async Task<Cinema> GetByIdAsync(int id)
        {
            var res = await _unitOfWork.Cinemas.FindByIdAsync(id);
            return res;
        }
    }
}
