using Microsoft.AspNetCore.Mvc;
using OrderCreator.API.Contracts;
using OrderCreator.Application.Services;
using OrderCreator.Core.Models;

namespace OrderCreator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrderById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _ordersService.GetOrderById(id, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            return Ok(await _ordersService.GetAllOrders(cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrdersRequest request, CancellationToken cancellationToken)
        {
            var (order, error) = Order.Create(
               Guid.NewGuid(),
               request.FromCity,
               request.FromAddress,
               request.ToCity,
               request.ToAddress,
               request.Weight,
               request.PickupDate);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var orderId = await _ordersService.CreateOrder(order, cancellationToken);

            return Ok(orderId);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrdersRequest request, CancellationToken cancellationToken)
        {

            var (order, error) = Order.Create(
               Guid.NewGuid(),
               request.FromCity,
               request.FromAddress,
               request.ToCity,
               request.ToAddress,
               request.Weight,
               request.PickupDate);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var orderId = await _ordersService.UpdateOrder(id, order, cancellationToken);

            return Ok(orderId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _ordersService.DeleteOrder(id, cancellationToken));
        }
    }
}
