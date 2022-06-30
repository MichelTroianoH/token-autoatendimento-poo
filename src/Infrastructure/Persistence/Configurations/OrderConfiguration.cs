using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("pedidos");

        builder.Property(t => t.Code)
            .IsRequired();

        builder.Property(t => t.Obs)
            .HasMaxLength(255);

        builder.Property(t => t.TotalValue)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(t => t.DiscountValue)
            .HasColumnType("decimal(10,2)");

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Cart)
            .HasForeignKey(x => x.CartId);
    }
}