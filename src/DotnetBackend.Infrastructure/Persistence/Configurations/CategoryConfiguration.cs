using DotnetBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetBackend.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Table name
        builder.ToTable("Categories");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);
            
        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(x => x.IsActive)
        .IsRequired()
        .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
         .IsRequired();

        // UpdatedAt
        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}
