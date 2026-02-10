using MediatR;
using Microsoft.AspNetCore.Mvc;
using Replate.Application.Features.FoodListings.Commands.CreateFoodListing;
using Replate.Application.Features.FoodListings.Commands.DeleteFoodListing;
using Replate.Application.Features.FoodListings.Commands.UpdateFoodListing;
using Replate.Application.Features.FoodListings.DTOs;
using Replate.Application.Features.FoodListings.Queries.GetAllFoodListings;
using Replate.Application.Features.FoodListings.Queries.GetFoodListingById;

namespace Replate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FoodListingsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Create a new food listing
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(FoodListingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateFoodListing(
        [FromQuery] int vendorProfileId,
        [FromBody] CreateFoodListingDto dto)
    {
        var command = new CreateFoodListingCommand
        {
            VendorProfileId = vendorProfileId,
            FoodListing = dto
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
            nameof(GetFoodListingById),
            new { publicId = result.Data!.PublicId },
            result.Data);
    }

    /// <summary>
    /// Get all food listings
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<FoodListingDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllFoodListings()
    {
        var result = await mediator.Send(new GetAllFoodListingsQuery());

        if (!result.IsSuccess)
        {
            return BadRequest(new { error = result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Get food listing by public ID
    /// </summary>
    [HttpGet("{publicId:guid}")]
    [ProducesResponseType(typeof(FoodListingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFoodListingById(Guid publicId)
    {
        var query = new GetFoodListingByIdQuery { PublicId = publicId };
        var result = await mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(new { error = result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Update a food listing
    /// </summary>
    [HttpPut("{publicId:guid}")]
    [ProducesResponseType(typeof(FoodListingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateFoodListing(
        Guid publicId,
        [FromBody] UpdateFoodListingDto dto)
    {
        var command = new UpdateFoodListingCommand
        {
            PublicId = publicId,
            FoodListing = dto
        };

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            if (result.ErrorMessage == "Food listing not found")
            {
                return NotFound(new { error = result.ErrorMessage });
            }
            return BadRequest(new { error = result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Delete a food listing
    /// </summary>
    [HttpDelete("{publicId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFoodListing(Guid publicId)
    {
        var command = new DeleteFoodListingCommand { PublicId = publicId };
        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            return NotFound(new { error = result.ErrorMessage });
        }

        return NoContent();
    }
}
