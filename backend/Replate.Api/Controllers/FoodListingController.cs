using MediatR;
using Microsoft.AspNetCore.Mvc;
using Replate.Application.Features.FoodListings.Commands.CreateFoodListing;
using Replate.Application.Features.FoodListings.Commands.DeleteFoodListing;
using Replate.Application.Features.FoodListings.Commands.UpdateFoodListing;
using Replate.Application.Features.FoodListings.DTOs;
using Replate.Application.Features.FoodListings.Queries.GetFoodListingByIdQuery;
using Replate.Application.Features.FoodListings.Queries.GetAllFoodListingQuery;

namespace Replate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FoodListingsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public FoodListingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new food listing.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateFoodListing([FromBody] CreateFoodListingDto request)
    {
        var command = new CreateFoodListingCommand { CreateFoodListingDto = request };
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
      
        return CreatedAtAction(nameof(GetFoodListingById), new { id = result.Data.PublicId }, result.Data);
    }

    /// <summary>
    /// Gets all food listings.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllFoodListings()
    {
        var result = await _mediator.Send(new GetAllFoodListingQuery());
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok(result.Data);
    }

    /// <summary>
    /// Gets a food listing by its public ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFoodListingById([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetFoodListingById { Id = id });
        if (result == null || !result.IsSuccess)
            return NotFound(result?.ErrorMessage ?? "Food listing not found");
        return Ok(result.Data);
    }

    /// <summary>
    /// Updates an existing food listing.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateFoodListing([FromRoute] Guid id, [FromBody] UpdateFoodListingDto request)
    {
        var command = new UpdateFoodListingCommand { FoodListingId = id, UpdateFoodListingDto = request };
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
        {
            if (result.ErrorMessage == "Food listing not found")
                return NotFound(result.ErrorMessage);
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Data);
    }
    
    
    /// <summary>
    /// Deletes a food listing by its public ID.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFoodListing([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteFoodListingCommand { PublicId = id });
        if (!result.IsSuccess)
            return NotFound(result.ErrorMessage);
        return NoContent();
    }
}