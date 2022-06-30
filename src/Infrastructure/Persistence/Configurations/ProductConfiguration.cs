using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("produtos");

        builder.Property(t => t.Name)
            .HasMaxLength(65)
            .IsRequired();

        builder.Property(t => t.Description);
        
        builder.Property(t => t.UnitPrice)
            .HasColumnType("decimal(10,2)")
            .IsRequired();
    }
}