using MediatR;
using Microsoft.AspNetCore.Mvc;
using Replate.Application.Features.FoodListingItems.Commands.CreateFoodListingItem;
using Replate.Application.Features.FoodListingItems.Commands.DeleteFoodListingItem;
using Replate.Application.Features.FoodListingItems.Commands.UpdateFoodListingItem;
using Replate.Application.Features.FoodListingItems.DTOs;
using Replate.Application.Features.FoodListingItems.Queries.GetFoodListingItemById;
using Replate.Application.Features.FoodListingItems.Queries.GetFoodListingItemsByFoodListing;

namespace Replate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FoodListingItemsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Create a new food listing item
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(FoodListingItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateFoodListingItem([FromBody] CreateFoodListingItemDto dto)
    {
        var command = new CreateFoodListingItemCommand
        {
            FoodListingItem = dto
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
            nameof(GetFoodListingItemById),
            new { id = result.Data!.Id },
            result.Data);
    }

    /// <summary>
    /// Get food listing item by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FoodListingItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFoodListingItemById(int id)
    {
        var query = new GetFoodListingItemByIdQuery { Id = id };
        var result = await mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(new { error = result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Get all food listing items for a specific food listing
    /// </summary>
    [HttpGet("by-food-listing/{foodListingPublicId:guid}")]
    [ProducesResponseType(typeof(List<FoodListingItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFoodListingItemsByFoodListing(Guid foodListingPublicId)
    {
        var query = new GetFoodListingItemsByFoodListingQuery { FoodListingPublicId = foodListingPublicId };
        var result = await mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(new { error = result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Update a food listing item
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(FoodListingItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateFoodListingItem(int id, [FromBody] UpdateFoodListingItemDto dto)
    {
        var command = new UpdateFoodListingItemCommand
        {
            Id = id,
            FoodListingItem = dto
        };

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            if (result.ErrorMessage == "Food listing item not found")
            {
                return NotFound(new { error = result.ErrorMessage });
            }
            return BadRequest(new { error = result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Delete a food listing item
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFoodListingItem(int id)
    {
        var command = new DeleteFoodListingItemCommand { Id = id };
        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            return NotFound(new { error = result.ErrorMessage });
        }

        return NoContent();
    }
}
