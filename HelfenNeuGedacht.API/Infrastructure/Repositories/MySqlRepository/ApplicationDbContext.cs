using Microsoft.EntityFrameworkCore;

namespace HelfenNeuGedacht.API.Infrastructure.Repositories.MySqlRepository
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Events> Event { get; set; }
        public DbSet<Shifts> Shift { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
