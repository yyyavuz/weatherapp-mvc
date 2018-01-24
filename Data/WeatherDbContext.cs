using Microsoft.EntityFrameworkCore;

namespace Kontrolmatik.Data
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
        : base(options)
        {

        }

        public DbSet<Tables.Weather> Weathers { get; set; }
        public DbSet<Tables.User> Users { get; set; }
    }
}