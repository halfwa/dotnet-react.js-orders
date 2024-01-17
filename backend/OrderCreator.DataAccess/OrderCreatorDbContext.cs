using Microsoft.EntityFrameworkCore;
using OrderCreator.DataAccess.Configurations;
using OrderCreator.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.DataAccess
{
    public class OrderCreatorDbContext: DbContext
    {
        public DbSet<OrderEntity> Orders { get; set; }

        public OrderCreatorDbContext(DbContextOptions<OrderCreatorDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new OrderConfiguration());
        }
    }
}
