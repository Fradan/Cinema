using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;

namespace Application
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<int> AddSessionAsync(Session session)
        {
            var sameSessions = await _unitOfWork.Sessions
                .FindSessionByParametersAsync(session.SessionTime, session.CinemaId, session.FilmId) ?? new List<Session>(0);

            if (sameSessions.Any())
            {
                throw new BusinessRuleValidationException("Несколько одинаковых сеансов");
            }
            await _unitOfWork.Sessions.AddAsync(session);
            await _unitOfWork.SaveAsync();
            return session.Id;
        }

        public async Task UpdateSessionAsync(int id, Session session)
        {
            var curSession = await _unitOfWork.Sessions.FindByIdAsync(id)
                ?? throw new BusinessRuleValidationException("Сеанс не найден");

            curSession.FilmId = session.FilmId;
            curSession.CinemaId = session.CinemaId;
            curSession.SessionTime = session.SessionTime;

            _unitOfWork.Sessions.Update(curSession);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteSessionAsync(int id)
        {
            var session = await _unitOfWork.Sessions.FindByIdAsync(id)
                ?? throw new BusinessRuleValidationException("Сеанс не найден");

            _unitOfWork.Sessions.Delete(session);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Session>> FindByDateAsync(DateTime date)
        {
            return await _unitOfWork.Sessions.FindByDateAsync(date);
        }

        public async Task<Session> GetByIdAsync(int id)
        {
            var session = await _unitOfWork.Sessions.FindByIdAsync(id);
            return session;
        }
    }
}
