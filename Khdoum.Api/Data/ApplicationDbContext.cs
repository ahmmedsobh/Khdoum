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
   // DbContext
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
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
        public DbSet<GeneralDelivery> GeneralDeliveries { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotifications> UserNotifications { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<UserCoupon> UserCoupons { get; set; }
        public DbSet<ProductOffer> ProductOffers { get; set; }
        public DbSet<Setting> Settings { get; set; }


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

            //UserNotifications
            modelBuilder.Entity<UserNotifications>()
                .HasKey(UN => new { UN.UserId, UN.NotificationId });


            modelBuilder.Entity<UserNotifications>()
                .HasOne(UN => UN.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(UN => UN.UserId)
                .IsRequired();

            modelBuilder.Entity<UserNotifications>()
                .HasOne(UN => UN.Notification)
                .WithMany(n => n.Notifications)
                .HasForeignKey(UN => UN.NotificationId)
                .IsRequired();

            //UserCoupon
            modelBuilder.Entity<UserCoupon>()
                .HasKey(UC => new { UC.UserId, UC.CouponId });


            modelBuilder.Entity<UserCoupon>()
                .HasOne(UC => UC.User)
                .WithMany(u => u.UserCoupons)
                .HasForeignKey(UN => UN.UserId)
                .IsRequired();

            modelBuilder.Entity<UserCoupon>()
                .HasOne(UC => UC.Coupon)
                .WithMany(c => c.UserCoupons)
                .HasForeignKey(UC => UC.CouponId)
                .IsRequired();


            modelBuilder.Entity<ApplicationUser>()
                        .HasMany(u => u.Orders)
                        .WithOne(o => o.User);

            modelBuilder.Entity<ApplicationUser>()
                        .HasMany(u => u.DeliveryOrders)
                        .WithOne(o => o.Delivery);

            modelBuilder.Entity<State>()
                        .HasMany(u => u.Orders)
                        .WithOne(o => o.State);

            modelBuilder.Entity<State>()
                        .HasMany(u => u.ToOrders)
                        .WithOne(o => o.ToState);

            modelBuilder.Entity<State>()
                       .HasMany(s => s.ToGeneralDeliveries)
                       .WithOne(g => g.ToState);

            modelBuilder.Entity<State>()
                        .HasMany(u => u.FromGeneralDeliveries)
                        .WithOne(o => o.FromState);

            modelBuilder.Entity<MarketProducts>()
                        .HasMany(m => m.ProductOffers)
                        .WithOne(o => o.MarketProducts);

            modelBuilder.Entity<Product>().Property(e => e.QuantityDuration).HasPrecision(18, 3);


            base.OnModelCreating(modelBuilder);
        }
    }
}
