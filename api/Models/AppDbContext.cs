using api.Models.Portoflios;
using api.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {

        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Collector> Collectors { get; set; }
        //public DbSet<Media> Files { get; set; }
        public DbSet<PortoflioMedia> PortoflioMedia { get; set; }
        public DbSet<Portoflio> Portoflios { get; set; }
    }
}