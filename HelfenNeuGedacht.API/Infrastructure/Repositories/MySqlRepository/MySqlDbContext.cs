using HelfenNeuGedacht.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Google.Protobuf.Compiler.CodeGeneratorResponse.Types;

namespace HelfenNeuGedacht.API.Infrastructure.Repositories.MySqlRepository
{
    public class MySqlDbContext : DbContext
    {
      
        public DbSet<Shift> Shifts { get; set; }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) { }
    }
}
