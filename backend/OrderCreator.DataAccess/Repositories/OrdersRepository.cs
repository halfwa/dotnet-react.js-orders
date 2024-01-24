using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderCreator.Core.Abstractions;
using OrderCreator.Core.Models;
using OrderCreator.DataAccess.Entities;

namespace OrderCreator.DataAccess.Repositories
{
    public sealed class OrdersRepository: IOrdersRepository
    {
        private readonly OrderCreatorDbContext _context;
        private readonly ILogger<OrdersRepository> _logger;

        public OrdersRepository(
            OrderCreatorDbContext context,
            ILogger<OrdersRepository> logger)
        {
            _context = context;
            _logger = logger; 
        }

        public async Task<Order> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} GetById function error", typeof(OrdersRepository));
                throw;
            }
        }

        public async Task<List<Order>> Get(CancellationToken cancellationToken)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Get function error", typeof(OrdersRepository));
                throw;
            }
        }

        public async Task<Guid> Create(Order order, CancellationToken cancellationToken)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Create function error", typeof(OrdersRepository));
                throw;
            }
        }

        public async Task<Guid> Update(Guid id, Order order, CancellationToken cancellationToken)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Update function error", typeof(OrdersRepository));
                throw;
            }
        }

        public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Orders.Where(o => o.Id == id)
                 .ExecuteDeleteAsync();

                return id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Delete function error", typeof(OrdersRepository));
                throw;
            }
        }
    }
}
