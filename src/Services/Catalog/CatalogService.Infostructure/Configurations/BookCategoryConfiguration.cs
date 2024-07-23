using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
{
    public void Configure(EntityTypeBuilder<BookCategory> builder)
    {
        /*builder
            .ToTable("book_category");*/

        builder
            .HasKey(bc => new { bc.BookId, bc.CategoryId });

        builder
            .HasOne(bc => bc.Book)
            .WithMany(b => b.BookCategories)
            .HasForeignKey(bc => bc.BookId);

        builder
            .HasOne(bc => bc.Category)
            .WithMany(c => c.BookCategories)
            .HasForeignKey(bc => bc.CategoryId);

        builder
            .Property(bc => bc.BookId)
            .HasColumnType("UUID")
            .HasColumnName("book_id");

        builder
            .Property(bc => bc.CategoryId)
            .HasColumnType("UUID")
            .HasColumnName("category_id");
    }
}
