using MediatR;
using Microsoft.AspNetCore.Mvc;
using Replate.Application.Features.Reservations.Commands.CreateReservation;
using Replate.Application.Features.Reservations.DTOs;

namespace Replate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ReservationsController : Controller
{
   private readonly IMediator _mediator;

   public ReservationsController(IMediator mediator)
   {
      _mediator = mediator;
   }
   
   /// <summary>
   /// Creates a new reservation for a food listing.
   /// </summary>
   [HttpPost("{foodListingId}/reservations")]
   [ProducesResponseType(StatusCodes.Status201Created)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> CreateReservation([FromRoute] Guid foodListingId, [FromBody] CreateReservationDto request)
   {
      var command = new CreateReservationCommand
      {
         UserId = request.UserId,
         FoodListingPublicId = foodListingId
      };
      var result = await _mediator.Send(command);
      if (!result.IsSuccess)
         return BadRequest(result.ErrorMessage);
      return CreatedAtAction(nameof(CreateReservation), new { id = result.Data?.PublicId }, result.Data);
   }
   
   [HttpGet("{foodListingId}/reservations/{reservationId}")]
   public async Task<IActionResult> GetReservation([FromRoute] Guid foodListingId, [FromRoute] Guid reservationId)
   {
      // Implementation for retrieving a reservation by foodListingId and reservationId
      return Ok();
   }
   
   
 
}