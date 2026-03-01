using Microsoft.EntityFrameworkCore;
using Rahul.StateWeatherForecast.Api.Models;

namespace Rahul.StateWeatherForecast.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<RawJson> RawJsons { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RawJson>()
                .ToTable("Json", "Raw");
        }
    }
}