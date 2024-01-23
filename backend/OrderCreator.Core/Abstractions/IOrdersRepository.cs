using OrderCreator.Core.Models;

namespace OrderCreator.Core.Abstractions
{
    public interface IOrdersRepository
    {
        Task<Guid> Create(Order order, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken);
        Task<List<Order>> Get(CancellationToken cancellationToken);
        Task<Order> GetById(Guid id, CancellationToken cancellationToken);
        Task<Guid> Update(Guid id, Order order, CancellationToken cancellationToken);
    }
}
