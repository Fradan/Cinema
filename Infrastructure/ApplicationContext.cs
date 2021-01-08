using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>();
            
            modelBuilder.Entity<Cinema>();
            modelBuilder.Entity<Session>()
                .HasOne<Cinema>().WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
