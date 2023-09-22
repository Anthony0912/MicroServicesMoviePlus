using Microsoft.EntityFrameworkCore;

namespace Service.api.Movie.DBConfig
{
    public class DBConnection
    {
        private readonly WebApplicationBuilder _build;

        public DBConnection(WebApplicationBuilder build)
        {
            _build = build;
        }

        public void Connect()
        {
            _build.Services.AddDbContext<DBMoviePlusContext>(options =>
            {
                options.UseSqlServer(_build.Configuration.GetConnectionString("ConnectionDB"));
            });
        }
    }
}
