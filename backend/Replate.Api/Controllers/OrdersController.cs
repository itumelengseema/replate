using MediatR;
using Microsoft.AspNetCore.Mvc;
using Replate.Application.Features.Reservations.Commands.CreateReservation;
using Replate.Application.Features.Reservations.DTOs;

namespace Replate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrdersController : Controller
{
   private readonly IMediator _mediator;

   public OrdersController(IMediator mediator)
   {
      _mediator = mediator;
   }
   
   /// <summary>
   /// Creates a new reservation for a food listing.
   /// </summary>
   [HttpPost("{foodListingId}/orders")]
   [ProducesResponseType(StatusCodes.Status201Created)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> CreateOrder([FromRoute] Guid foodListingId, [FromBody] CreateOrderDto request)
   {
      var command = new CreateReservationCommand
      {
         UserId = request.UserId,
         FoodListingPublicId = foodListingId
      };
      var result = await _mediator.Send(command);
      if (!result.IsSuccess)
         return BadRequest(result.ErrorMessage);
      return CreatedAtAction(nameof(CreateOrder), new { id = result.Data?.PublicId }, result.Data);
   }
   
   [HttpGet("{foodListingId}/order/{reservationId}")]
   public async Task<IActionResult> GetOrders([FromRoute] Guid foodListingId, [FromRoute] Guid reservationId)
   {
      // Implementation for retrieving a reservation by foodListingId and reservationId
      return Ok();
   }
   
   
 
}