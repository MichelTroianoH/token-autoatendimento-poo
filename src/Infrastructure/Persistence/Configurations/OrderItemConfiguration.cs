using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("pedido_item");

        builder.Property(t => t.TotalValue)
            .HasColumnType("decimal(10,2)")
            .IsRequired();
        
        builder.Property(t => t.Amount)
            .IsRequired();

        builder.HasOne(x => x.Product);
    }
}