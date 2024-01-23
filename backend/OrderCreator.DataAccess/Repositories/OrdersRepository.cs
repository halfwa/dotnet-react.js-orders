using Microsoft.EntityFrameworkCore;
using OrderCreator.Core.Abstractions;
using OrderCreator.Core.Models;
using OrderCreator.DataAccess.Entities;

namespace OrderCreator.DataAccess.Repositories
{
    public sealed class OrdersRepository(OrderCreatorDbContext _context) : IOrdersRepository
    {
        public async Task<Order> GetById(Guid id, CancellationToken cancellationToken)
        {
            var orderEntity = await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            var order = Order.Create(
                orderEntity!.Id,
                orderEntity.FromCity, orderEntity.FromAddress,
                orderEntity.ToCity, orderEntity.ToAddress,
                orderEntity.Weight,
                orderEntity.PickupDate)
                    .Order;

            return order;
        }

        public async Task<List<Order>> Get(CancellationToken cancellationToken)
        {
            var orderEntities = await _context.Orders
                .AsNoTracking()
                .OrderBy(o => o.PickupDate)
                .ToListAsync(cancellationToken);

            var orders = orderEntities
                .Select(o => Order.Create(
                    o.Id,
                    o.FromCity, o.FromAddress,
                    o.ToCity, o.ToAddress,
                    o.Weight,
                    o.PickupDate)
                        .Order)
                .ToList();

            return orders;
        }

        public async Task<Guid> Create(Order order, CancellationToken cancellationToken)
        {
            var orderEntity = new OrderEntity
            {
                Id = order.Id,
                FromCity = order.FromCity,
                FromAddress = order.FromAddress,
                ToCity = order.ToCity,
                ToAddress = order.ToAddress,
                Weight = order.Weight,
                PickupDate = order.PickupDate
            };

            await _context.Orders.AddAsync(orderEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return orderEntity.Id;
        }

        public async Task<Guid> Update(Guid id, Order order, CancellationToken cancellationToken)
        {

            await _context.Orders
                .Where(o => o.Id == id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(o => o.FromCity, o => order.FromCity)
                    .SetProperty(o => o.FromAddress, o => order.FromAddress)
                    .SetProperty(o => o.ToCity, o => order.ToCity)
                    .SetProperty(o => o.ToAddress, o => order.ToAddress)
                    .SetProperty(o => o.Weight, o => order.Weight)
                    .SetProperty(o => o.PickupDate, o => order.PickupDate), cancellationToken);

            return id;
        }

        public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _context.Orders
                .Where(o => o.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
