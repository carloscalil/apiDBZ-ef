using Microsoft.EntityFrameworkCore;
using ProjetoDBZ.Models;

namespace ProjetoDBZ.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Personagem> DBZ { get; set; }
    }
}
