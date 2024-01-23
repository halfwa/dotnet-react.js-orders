using Microsoft.EntityFrameworkCore;
using OrderCreator.DataAccess.Configurations;
using OrderCreator.DataAccess.Entities;

namespace OrderCreator.DataAccess
{
    public class OrderCreatorDbContext: DbContext
    {
        public DbSet<OrderEntity> Orders { get; set; }

        public OrderCreatorDbContext(DbContextOptions<OrderCreatorDbContext> options)
            : base(options) 
        { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new OrderConfiguration());
        }
    }
}
