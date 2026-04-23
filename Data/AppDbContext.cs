using ApiReuniao.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiReuniao.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Sala> Salas { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
