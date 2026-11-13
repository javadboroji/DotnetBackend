using DotnetBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetBackend.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
           .IsRequired()
           .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(2000);

        builder.Property(x => x.Price)
         .IsRequired()
         .HasPrecision(18, 2);

        builder.Property(x => x.IsActive)
          .IsRequired()
          .HasDefaultValue(true);

        builder.Property(x => x.CategoryId)
        .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.HasOne(x => x.Category)
          .WithMany(x => x.Products)
          .HasForeignKey(x => x.CategoryId)
          .OnDelete(DeleteBehavior.Restrict);

    }
}
// *HasPrecision
// *DeleteBehavior.Restrict