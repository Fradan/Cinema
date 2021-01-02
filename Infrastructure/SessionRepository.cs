using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;

namespace Infrastructure
{
    public class SessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        public SessionRepository(ApplicationContext context) : base(context) { }

        public async Task<List<Session>> FindByDateAsync(DateTime sessionDate)
        {
            return await _dbSet.Where(x => x.SessionTime.Date == sessionDate.Date).ToListAsync();
        }

        public async Task<List<Session>> FindSessionByParametersAsync(DateTime sessionTime, int? cinemaId = null, int? filmId = null)
        {
            var query = _dbSet.Where(x => x.SessionTime == sessionTime);

            if (cinemaId.HasValue)
            {
                query = query.Where(x => x.CinemaId == cinemaId);
            }

            if (filmId.HasValue)
            {
                query = query.Where(x => x.FilmId == filmId);
            }

            return await query.ToListAsync();
        }
    }
}
