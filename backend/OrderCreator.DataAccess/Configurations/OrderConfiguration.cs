using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderCreator.DataAccess.Entities;

namespace OrderCreator.DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>

    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(o => o.FromCity)
                .IsRequired();

            builder.Property(o => o.FromAddress)
                .IsRequired();

            builder.Property(o => o.ToCity)
                .IsRequired();

            builder.Property(o => o.ToAddress)
                .IsRequired();

            builder.Property(o => o.Weight)
                .HasPrecision(6, 2)
                .IsRequired();
        }
    }
}
