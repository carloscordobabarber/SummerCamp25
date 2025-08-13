using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CozyData
{
    public class ContextDataBase : DbContext
    {
        public ContextDataBase(DbContextOptions<ContextDataBase> options): base(options){ }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Building> Buildings { get; set; }

        public DbSet<District> Districts { get; set; } 
        public DbSet<DistrictStreet> DistrictStreets { get; set; } 
        
        public DbSet<Incidence> Incidences { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Street> Streets { get; set; } 

        public DbSet<User> Users { get; set; } 

    }
}
