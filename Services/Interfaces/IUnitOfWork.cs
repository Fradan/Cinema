using Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork
    {
        Task SaveAsync();

        ISessionRepository Sessions { get; }
        ICinemaRepository Cinemas { get; }
        IFilmRepository Films { get; }
    }
}
