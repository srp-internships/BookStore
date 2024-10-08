﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentService.Infrastructure.Persistence;

#nullable disable

namespace PaymentService.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("payment_service")
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PaymentService.Application.Common.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("inbox_messages", "payment_service");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.Cards.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CardCvc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("CardExpirationDate")
                        .HasColumnType("date");

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CardHolderRole")
                        .HasColumnType("int");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("cards", "payment_service");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.Payments.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ProcessedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RequestedAtUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("payments", "payment_service");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.Payments.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("PaymentId");

                    b.ToTable("transactions", "payment_service");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.Payments.Transaction", b =>
                {
                    b.HasOne("PaymentService.Domain.Entities.Cards.Card", "Card")
                        .WithMany("Transactions")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentService.Domain.Entities.Payments.Payment", "Payment")
                        .WithMany("Transaction")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.Cards.Card", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.Payments.Payment", b =>
                {
                    b.Navigation("Transaction");
                });
#pragma warning restore 612, 618
        }
    }
}
