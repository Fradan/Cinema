using Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public interface ISessionService
    {
        Task<int> AddSessionAsync(Session session);

        Task UpdateSessionAsync(int id, Session session);

        Task DeleteSessionAsync(int id);

        Task<Session> GetByIdAsync(int id);

        Task<List<Session>> FindByDateAsync(DateTime date);
    }
}
