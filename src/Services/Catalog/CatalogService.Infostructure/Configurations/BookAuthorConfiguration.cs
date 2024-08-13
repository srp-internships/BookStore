using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        builder
            .ToTable("book_author");

        builder
            .Property(ba => ba.BookId)
            .HasColumnType("UUID")
            .HasColumnName("book_id");

        builder
            .Property(ba => ba.AuthorId)
            .HasColumnType("UUID")
            .HasColumnName("author_id");


    }
}
