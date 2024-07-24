﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;


namespace OrderService.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.OrderDate).IsRequired();

        builder.HasOne<Customer>(i => i.Customer)
          .WithMany(o=>o.Orders)
          .HasForeignKey(o => o.CustomerId)
          .IsRequired();

        builder.HasMany(o => o.Items)
            .WithOne(o => o.Order)
            .HasForeignKey(oi => oi.OrderId);


        builder.ComplexProperty(
           o => o.ShippingAddress, addressBuilder =>
           {
               addressBuilder.Property(a => a.FirstName)
                   .HasMaxLength(50);

               addressBuilder.Property(a => a.LastName)
                    .HasMaxLength(50);

               addressBuilder.Property(a => a.EmailAddress)
                   .HasMaxLength(50);

               addressBuilder.Property(a => a.Country)
                   .HasMaxLength(50);

               addressBuilder.Property(a => a.State)
                   .HasMaxLength(50);

               addressBuilder.Property(a => a.ZipCode)
                   .HasMaxLength(5);
                   
           });

        builder.ComplexProperty(
               o => o.Payment, paymentBuilder =>
               {
                   paymentBuilder.Property(p => p.CardName)
                       .HasMaxLength(50);

                   paymentBuilder.Property(p => p.CardNumber)
                       .HasMaxLength(24);

                   paymentBuilder.Property(p => p.Expiration)
                       .HasMaxLength(10);

                   paymentBuilder.Property(p => p.Cvv)
                       .HasMaxLength(3);
               });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.Property(o => o.TotalPrice);
    }
}
