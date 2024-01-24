using Microsoft.AspNetCore.Mvc;
using OrderCreator.API.Contracts;
using OrderCreator.Application.Services;
using OrderCreator.Core.Models;

namespace OrderCreator.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IOrdersService _ordersService) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrderById(Guid id, CancellationToken cancellationToken)
        {
            var orders = await _ordersService.GetOrderById(id, cancellationToken);

            if (orders is null) 
            {
                return NoContent();
            }

            return Ok(orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            var order = await _ordersService.GetAllOrders(cancellationToken);

            if (order is null)
            {
                return NoContent();
            }

            return Ok(order);
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

            if (string.IsNullOrEmpty(orderId.ToString()))
            {
                return StatusCode(500, "Internal error");
            }

            return CreatedAtAction(nameof(GetOrderById), new { Id = orderId}, order);
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

            if (string.IsNullOrEmpty(orderId.ToString()))
            {
                return StatusCode(500, "Internal error");
            }

            return Ok("Successfully updated!");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id, CancellationToken cancellationToken)
        {
            var orderId = await _ordersService.DeleteOrder(id, cancellationToken);

            if (string.IsNullOrEmpty(orderId.ToString()))
            {
                return StatusCode(500, "Internal error");
            }

            return NoContent();
        }
    }
}
