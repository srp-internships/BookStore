using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infostructure.Configurations
{
    public sealed class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .ToTable("book");

            builder
                .Property(p => p.Id)
                .HasColumnType("UUID")
                .HasColumnName("id")
                .IsRequired();

            builder
                .HasKey(k => k.Id);

            builder
                .Property(p => p.Title)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(200)")
                .IsRequired();

            builder
                .Property(p => p.Image)
                .HasColumnName("image")
                .HasColumnType("VARCHAR(500)")
                .IsRequired();

            builder
                .HasOne(p => p.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(fk => fk.PublisherId);
            builder
            .Property(p => p.PublisherId)
            .HasColumnName("publisher_id");

            builder
            .HasMany(c => c.Categories)
            .WithMany(s => s.Books)
            .UsingEntity<BookCategory>(
               j => j
                .HasOne(pt => pt.Category)
                .WithMany(t => t.BookCategories)
                .HasForeignKey(pt => pt.CategoryId),
               j => j
                .HasOne(pt => pt.Book)
                .WithMany(p => p.BookCategories)
                .HasForeignKey(pt => pt.BookId),
               j =>
               {
                   j.HasKey(t => new { t.CategoryId, t.BookId });
               }
            );

            builder
            .HasMany(c => c.Authors)
            .WithMany(s => s.Books)
            .UsingEntity<BookAuthor>(
               j => j
                .HasOne(pt => pt.Author)
                .WithMany(t => t.BookAuthors)
                .HasForeignKey(pt => pt.AuthorId),
               j => j
                .HasOne(pt => pt.Book)
                .WithMany(p => p.BookAuthors)
                .HasForeignKey(pt => pt.BookId),
              j =>
              {
                  j.HasKey(t => new { t.AuthorId, t.BookId });
              }
            );
            builder
            .HasMany(c => c.Sellers)
            .WithMany(s => s.Books)
            .UsingEntity<BookSeller>(
               j => j
                .HasOne(pt => pt.Seller)
                .WithMany(t => t.BookSellers)
                .HasForeignKey(pt => pt.SellerId),
               j => j
                .HasOne(pt => pt.Book)
                .WithMany(p => p.BookSellers)
                .HasForeignKey(pt => pt.BookId),
               j =>
               {
                j.HasKey(t => new { t.SellerId, t.BookId });
               }
            );
        }
    }
}
