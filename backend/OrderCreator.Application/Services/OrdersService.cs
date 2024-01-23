using OrderCreator.Core.Abstractions;
using OrderCreator.Core.Models;

namespace OrderCreator.Application.Services
{
    public class OrdersService(IOrdersRepository _ordersRepository) : IOrdersService
    {
        public async Task<List<Order>> GetAllOrders(CancellationToken cancellationToken)
        {
            return await _ordersRepository.Get(cancellationToken);
        }

        public async Task<Order> GetOrderById(Guid id, CancellationToken cancellationToken)
        {
            return await _ordersRepository.GetById(id, cancellationToken);
        }

        public async Task<Guid> CreateOrder(Order order, CancellationToken cancellationToken)
        {
            return await _ordersRepository.Create(order, cancellationToken);
        }

        public async Task<Guid> UpdateOrder(Guid id, Order order, CancellationToken cancellationToken)
        {
            return await _ordersRepository.Update(id, order, cancellationToken);
        }

        public async Task<Guid> DeleteOrder(Guid id, CancellationToken cancellationToken)
        {
            return await _ordersRepository.Delete(id, cancellationToken);
        }
    }
}
