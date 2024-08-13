﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShipmentService.Infrastructure.Persistence.DbContexts;

#nullable disable

namespace ShipmentService.Infrastructure.Migrations
{
    [DbContext(typeof(ShipmentContext))]
    [Migration("20240731113239_ChangeSetDb")]
    partial class ChangeSetDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShipmentService.Domain.Entities.Shipments.Shipment", b =>
                {
                    b.Property<Guid>("ShipmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ShippingAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateShipmentStatus")
                        .HasColumnType("datetime2");

                    b.HasKey("ShipmentId");

                    b.HasIndex("ShippingAddressId");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("ShipmentService.Domain.Entities.Shipments.ShipmentItem", b =>
                {
                    b.Property<Guid>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<Guid?>("ShipmentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ItemId");

                    b.HasIndex("ShipmentId");

                    b.ToTable("ShipmentItems");
                });

            modelBuilder.Entity("ShipmentService.Domain.Entities.Shipments.ShippingAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShippingAddresses");
                });

            modelBuilder.Entity("ShipmentService.Domain.Entities.Shipments.Shipment", b =>
                {
                    b.HasOne("ShipmentService.Domain.Entities.Shipments.ShippingAddress", "ShippingAddress")
                        .WithMany()
                        .HasForeignKey("ShippingAddressId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("ShippingAddress");
                });

            modelBuilder.Entity("ShipmentService.Domain.Entities.Shipments.ShipmentItem", b =>
                {
                    b.HasOne("ShipmentService.Domain.Entities.Shipments.Shipment", null)
                        .WithMany("Items")
                        .HasForeignKey("ShipmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShipmentService.Domain.Entities.Shipments.Shipment", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
