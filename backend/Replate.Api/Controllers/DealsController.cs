using MediatR;
using Microsoft.AspNetCore.Mvc;
using Replate.Application.Features.Deals.Commands.CreateDeal;
using Replate.Application.Features.Deals.Commands.DeleteDeal;
using Replate.Application.Features.Deals.Commands.PatchDeal;
using Replate.Application.Features.Deals.Commands.UpdateDeal;
using Replate.Application.Features.Deals.DTOs;
using Replate.Application.Features.Deals.Queries.GetAllDealsQuery;
using Replate.Application.Features.Deals.Queries.GetDealByIdQuery;

namespace Replate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DealsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public DealsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new deal.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDeal([FromBody] CreateDealDto request)
    {
        var command = new CreateDealCommand { CreateDealDto = request };
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
      
        return CreatedAtAction(nameof(GetDealById), new { id = result.Data.PublicId }, result.Data);
    }

    /// <summary>
    /// Gets all deals.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDeals()
    {
        var result = await _mediator.Send(new GetAllDealsQuery());
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok(result.Data);
    }

    /// <summary>
    /// Gets a deal by its public ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDealById([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetDealByIdQuery { Id = id });
        if (result == null || !result.IsSuccess)
            return NotFound(result?.ErrorMessage ?? "Deal not found");
        return Ok(result.Data);
    }

    /// <summary>
    /// Updates an existing deal.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDeal([FromRoute] Guid id, [FromBody] UpdateDealDto request)
    {
        var command = new UpdateDealCommand { DealId = id, UpdateDealDto = request };
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
        {
            if (result.ErrorMessage == "Deal not found")
                return NotFound(result.ErrorMessage);
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Data);
    }
    
    
    /// <summary>
    ///  Partially updates an existing deal.
    ///  </summary>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    
    public async Task<IActionResult> PatchDeal([FromRoute] Guid id, [FromBody] PatchDealDto request)
    {
        if (id != request.PublicId)
        {
            return BadRequest("ID in URL does not match ID in body.");
        }
        var command = new PatchDealCommand { DealId = id, PatchDealDto = request };
        var result = await _mediator.Send(command); 

        if (!result.IsSuccess)
        {
            if (result?.ErrorMessage == "Deal not found")
                return NotFound(result.ErrorMessage);
            return BadRequest(result?.ErrorMessage ?? "An error occurred.");
        }
        Console.WriteLine("Patched Deal: " + result.Data);
        return Ok(result.Data);
    }

    /// <summary>
    /// Deletes a deal by its public ID.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDeal([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteDealCommand { PublicId = id });
        if (!result.IsSuccess)
            return NotFound(result.ErrorMessage);
        return NoContent();
    }
}