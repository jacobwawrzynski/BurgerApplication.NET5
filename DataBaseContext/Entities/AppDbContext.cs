using Microsoft.EntityFrameworkCore;
using DataBaseContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Entities
{
    public class AppDbContext : DbContext
    {
        string connectionString_ = "Data Source=DESKTOP-FRHKQQK;Initial Catalog=BurgerAppDataBase;Integrated Security=True";
         //string connectionString_ = @"Data Source=(localdb)\MSSqlLocalDb; Initial Catalog=BurgerAppDataBase; Integrated Security=True;";

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Discount_Code> Discount_Codes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Allergen> Products_Allergens { get; set; }
        public DbSet<Product_Order> Products_Orders { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString_);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product_Allergen>()
                .HasOne(p => p.Product)
                .WithMany(pa => pa.Products_Allergens)
                .HasForeignKey(pi => pi.Id_Product);
            modelBuilder.Entity<Product_Allergen>()
                .HasOne(a => a.Allergen)
                .WithMany(pa => pa.Products_Allergens)
                .HasForeignKey(ai => ai.Id_Allergen);

            modelBuilder.Entity<Product_Order>()
                .HasOne(p => p.Product)
                .WithMany(po => po.Products_Orders)
                .HasForeignKey(pi => pi.Id_Product);
            modelBuilder.Entity<Product_Order>()
                .HasOne(o => o.Order)
                .WithMany(po => po.Products_Orders)
                .HasForeignKey(oi => oi.Id_Order);
            modelBuilder.Entity<Address>(
                e =>
                {
                    e.Property(f => f.House_Number)
                    .HasColumnType("nvarchar(5)");
                });
                
        }
    }
}
