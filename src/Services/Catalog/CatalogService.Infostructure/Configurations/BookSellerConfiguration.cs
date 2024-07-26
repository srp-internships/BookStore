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
    public class BookSellerConfiguration : IEntityTypeConfiguration<BookSeller>
    {
        public void Configure(EntityTypeBuilder<BookSeller> builder)
        {
            builder
                .ToTable("book_seller");

            builder
                .Property(p => p.Id)
                .HasColumnType("UUID")
                .HasColumnName("id")
                .IsRequired();

            builder
                .HasKey(k => k.Id);

            builder
                .Property(p => p.Price)
                .HasColumnName("price")
                .HasColumnType("MONEY")
                .IsRequired();

            builder
                .Property(p => p.Description)
                .HasColumnName("description")
                .HasColumnType("VARCHAR(500)")
                .IsRequired(false);

            builder
                 .Property(bs => bs.BookId)
                 .HasColumnType("UUID")
                 .HasColumnName("book_id");

            builder
                .Property(bs => bs.SellerId)
                .HasColumnType("UUID")
                .HasColumnName("seller_id");    

        }
    }
}
