using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        /*builder
            .ToTable("book_author");*/

        builder
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

        builder
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookId);

        builder
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId);

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
