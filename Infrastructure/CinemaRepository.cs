using Core;
using Application;

namespace Infrastructure
{
    public class CinemaRepository : RepositoryBase<Cinema>,  ICinemaRepository
    {
        public CinemaRepository(ApplicationContext context) : base(context) { }
    }
}
