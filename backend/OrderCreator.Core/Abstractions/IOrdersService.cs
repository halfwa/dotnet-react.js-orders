using OrderCreator.Core.Models;

namespace OrderCreator.Application.Services
{
    public interface IOrdersService
    {
        Task<Guid> CreateOrder(Order order, CancellationToken cancellationToken);
        Task<Guid> DeleteOrder(Guid id, CancellationToken cancellationToken);
        Task<List<Order>> GetAllOrders(CancellationToken cancellationToken);
        Task<Order> GetOrderById(Guid id, CancellationToken cancellationToken);
        Task<Guid> UpdateOrder(Guid id, Order order, CancellationToken cancellationToken);
    }
}