using Khdoum.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Data
{
    //IdentityDbContext<ApplicationUser>
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<MarketProducts> MarketProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Unit> Units { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //OrderDetails
            modelBuilder.Entity<OrderDetails>()
                .HasKey(OD => new { OD.OrderId, OD.ProductId });


            modelBuilder.Entity<OrderDetails>()
                .HasOne(OD => OD.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(OD => OD.ProductId)
                .IsRequired();

            modelBuilder.Entity<OrderDetails>()
                .HasOne(OD => OD.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(OD => OD.OrderId)
                .IsRequired();

            //MarketProducts
            modelBuilder.Entity<MarketProducts>()
                .HasKey(MP => new { MP.UserId, MP.ProductId });


            modelBuilder.Entity<MarketProducts>()
                .HasOne(MP => MP.Product)
                .WithMany(p => p.MarketProducts)
                .HasForeignKey(MP => MP.ProductId)
                .IsRequired();

            modelBuilder.Entity<MarketProducts>()
                .HasOne(MP => MP.User)
                .WithMany(u => u.MarketProducts)
                .HasForeignKey(MP => MP.UserId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
