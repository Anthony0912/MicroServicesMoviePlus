using Microsoft.EntityFrameworkCore;
using Service.api.Movie.Entities;

namespace Service.api.Movie.DBConfig
{
    public class DBMoviePlusContext : DbContext
    {
        public DbSet<EMovie> Movie { get; set; }
        public DBMoviePlusContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EMovie>().ToTable("Movies");
        }
    }
}
