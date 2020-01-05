using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class PharmacyDBContext : DbContext
    {
        public PharmacyDBContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AgeCategory> AgeCategories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Concentration> Concentrations { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedMedicine> OrderedMedicines { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<WorkingHour> WorkingHours { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<AplicationMethod> AplicationMethods { get; set; }
        public DbSet<Size> Sizes { get; set; }

        public DbSet<BodyPart> BodyParts { get; set; }
    }
}