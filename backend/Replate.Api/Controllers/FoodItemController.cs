using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Replate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FoodItemController : Controller
{
    private readonly IMediator _mediator;
    
    public FoodItemController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateFoodItem()
    {
        return Ok();
    }
    
}