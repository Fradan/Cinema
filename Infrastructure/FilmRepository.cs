using Core;
using Application;

namespace Infrastructure
{
    public class FilmRepository : RepositoryBase<Film>, IFilmRepository
    {
        public FilmRepository(ApplicationContext context) : base(context) { }
    }
}
