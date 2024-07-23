﻿// <auto-generated />
using System;
using CatalogService.Infostructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CatalogService.Infostructure.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    [Migration("20240719135828_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CatalogService.Domain.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UUID")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("author", (string)null);
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UUID")
                        .HasColumnName("id");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("image");

                    b.Property<Guid>("PublisherId")
                        .HasColumnType("UUID")
                        .HasColumnName("publisher_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("book", (string)null);
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.BookAuthor", b =>
                {
                    b.Property<Guid>("AuthorId")
                        .HasColumnType("UUID")
                        .HasColumnName("author_id");

                    b.Property<Guid>("BookId")
                        .HasColumnType("UUID")
                        .HasColumnName("book_id");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.BookCategory", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .HasColumnType("UUID")
                        .HasColumnName("category_id");

                    b.Property<Guid>("BookId")
                        .HasColumnType("UUID")
                        .HasColumnName("book_id");

                    b.HasKey("CategoryId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookCategories");
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.BookSeller", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UUID")
                        .HasColumnName("id");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER")
                        .HasColumnName("amount");

                    b.Property<Guid>("BookId")
                        .HasColumnType("UUID")
                        .HasColumnName("book_id");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("description");

                    b.Property<decimal>("Price")
                        .HasColumnType("MONEY")
                        .HasColumnName("price");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("UUID")
                        .HasColumnName("seller_id");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("SellerId");

                    b.ToTable("bookseller", (string)null);
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UUID")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("category", (string)null);
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.Publisher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UUID")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("address");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("email");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("publisher", (string)null);
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.Seller", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UUID")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("seller", (string)null);
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.Book", b =>
                {
                    b.HasOne("CatalogService.Domain.Entities.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.BookAuthor", b =>
                {
                    b.HasOne("CatalogService.Domain.Entities.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatalogService.Domain.Entities.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.BookCategory", b =>
                {
                    b.HasOne("CatalogService.Domain.Entities.Book", "Book")
                        .WithMany("BookCategories")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatalogService.Domain.Entities.Category", "Category")
                        .WithMany("BookCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.BookSeller", b =>
                {
                    b.HasOne("CatalogService.Domain.Entities.Book", "Book")
                        .WithMany("BookSellers")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatalogService.Domain.Entities.Seller", "Seller")
                        .WithMany("BookSellers")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.Author", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.Book", b =>
                {
                    b.Navigation("BookAuthors");

                    b.Navigation("BookCategories");

                    b.Navigation("BookSellers");
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.Category", b =>
                {
                    b.Navigation("BookCategories");
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("CatalogService.Domain.Entities.Seller", b =>
                {
                    b.Navigation("BookSellers");
                });
#pragma warning restore 612, 618
        }
    }
}
