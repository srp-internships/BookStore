using Microsoft.EntityFrameworkCore;
using ShipmentService.Domain.Entities.Shipments;
using ShipmentService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Infrastructure.Persistence.DbContexts
{

    public class ShipmentContext : DbContext
    {
        public ShipmentContext(DbContextOptions<ShipmentContext> options) : base(options) { }

        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<ShipmentItem> ShipmentItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasKey(e => e.ShipmentId);

                entity.HasMany(e => e.Items)
                      .WithOne()
                      .HasForeignKey("ShipmentId")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ShippingAddress)
                      .WithMany()
                      .HasForeignKey("ShippingAddressId")
                      .OnDelete(DeleteBehavior.SetNull); 

                entity.Property(e => e.OrderStatus)
                      .HasConversion(
                          v => v.ToString(),
                          v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));
            });

            modelBuilder.Entity<ShippingAddress>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Street)
                      .IsRequired();

                entity.Property(e => e.City)
                      .IsRequired();

                entity.Property(e => e.Country)
                      .IsRequired();
            });

            modelBuilder.Entity<ShipmentItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.BookName)
                      .IsRequired();

                entity.Property(e => e.Quantity)
                      .IsRequired()
                      .HasDefaultValue(1);
            });
        }
    }
}
