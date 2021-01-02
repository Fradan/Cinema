using Application;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public ISessionRepository Sessions { get; }

        public ICinemaRepository Cinemas { get; }

        public IFilmRepository Films { get; }

        public UnitOfWork(ApplicationContext context, 
            ISessionRepository sessionRepository,
            IFilmRepository filmRepository,
            ICinemaRepository cinemaRepository)
        {
            _context = context;
            Cinemas = cinemaRepository;
            Films = filmRepository;
            Sessions = sessionRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
