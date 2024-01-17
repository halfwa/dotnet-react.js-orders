using OrderCreator.Core.Abstractions;
using OrderCreator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderCreator.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

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
