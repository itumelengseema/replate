using MediatR;
using Microsoft.AspNetCore.Mvc;
using Replate.Application.Features.Orders.Commands.CancelOrder;
using Replate.Application.Features.Orders.Commands.CreateOrder;
using Replate.Application.Features.Orders.Commands.UpdateOrder;
using Replate.Application.Features.Orders.DTOs;
using Replate.Application.Features.Orders.Queries.GetAllOrders;
using Replate.Application.Features.Orders.Queries.GetOrderById;
using Replate.Application.Features.Orders.Queries.GetOrdersByUser;

namespace Replate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrdersController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Create a new order
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder(
        [FromQuery] int userId,
        [FromBody] CreateOrderDto dto)
    {
        var command = new CreateOrderCommand
        {
            UserId = userId,
            Order = dto
        };

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            if (result.ValidationErrors != null && result.ValidationErrors.Any())
            {
                return BadRequest(new { errors = result.ValidationErrors });
            }
            return BadRequest(new { error = result.ErrorMessage });
        }

        return CreatedAtAction(
            nameof(GetOrderById),
            new { publicId = result.Data!.PublicId },
            result.Data);
    }

    /// <summary>
    /// Get all orders
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllOrders()
    {
        var result = await mediator.Send(new GetAllOrdersQuery());

        if (!result.IsSuccess)
        {
            return BadRequest(new { error = result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Get order by public ID
    /// </summary>
    [HttpGet("{publicId:guid}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(Guid publicId)
    {
        var query = new GetOrderByIdQuery { PublicId = publicId };
        var result = await mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(new { error = result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Get orders by user ID
    /// </summary>
    [HttpGet("user/{userId:int}")]
    [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersByUser(int userId)
    {
        var query = new GetOrdersByUserQuery { UserId = userId };
        var result = await mediator.Send(query);

        if (!result.IsSuccess)
        {
            return BadRequest(new { error = result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Update an order
    /// </summary>
    [HttpPut("{publicId:guid}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder(
        Guid publicId,
        [FromBody] UpdateOrderDto dto)
    {
        var command = new UpdateOrderCommand
        {
            PublicId = publicId,
            Order = dto
        };

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            if (result.ErrorMessage == "Order not found")
            {
                return NotFound(new { error = result.ErrorMessage });
            }
            return BadRequest(new { error = result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Cancel an order
    /// </summary>
    [HttpPost("{publicId:guid}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelOrder(Guid publicId)
    {
        var command = new CancelOrderCommand { PublicId = publicId };
        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            if (result.ErrorMessage == "Order not found")
            {
                return NotFound(new { error = result.ErrorMessage });
            }
            return BadRequest(new { error = result.ErrorMessage });
        }

        return NoContent();
    }
}
