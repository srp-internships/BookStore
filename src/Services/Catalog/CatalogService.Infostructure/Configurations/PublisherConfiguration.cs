using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infostructure.Configurations
{
    public sealed class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder
                .ToTable("publisher");

            builder
                .Property(p => p.Id)
                .HasColumnType("UUID")
                .HasColumnName("id")
                .IsRequired();

            builder
                .HasKey(k => k.Id);

            builder
                .Property(p => p.Name)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

            builder
                .Property(p => p.Email)
                .HasColumnName("email")
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            builder
                .Property(p => p.Address)
                .HasColumnName("address")
                .HasColumnType("VARCHAR(500)")
                .IsRequired();
        }
    }
}
