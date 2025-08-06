using Dominio;
using Microsoft.EntityFrameworkCore;

namespace CozyData
{
    public class ContextoAPI : DbContext
    {
        public ContextoAPI(DbContextOptions<ContextoAPI> options) : base(options)
        {
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public DbSet<Log> Logs { get; set; }
    }
}
