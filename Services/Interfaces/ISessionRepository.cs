using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Application
{
    public interface ISessionRepository : IRepositoryBase<Session>
    {
        Task<List<Session>> FindByDateAsync(DateTime date);

        Task<List<Session>> FindSessionByParametersAsync(DateTime sessionTime, int? cinemaId = null, int? filmId = null);
    }
}
