using System;

public class ContextoBaseDatos : DbContext
{
    publuc ContextoBaseDatos(DbContextOptions<ContextoBaseDatos> options) : base(options) { }
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Building> Buildings { get; set; }
}
