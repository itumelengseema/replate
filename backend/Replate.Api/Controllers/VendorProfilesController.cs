using MediatR;
using Microsoft.AspNetCore.Mvc;
using Replate.Application.Features.VendorProfiles.Commands.CreateVendorProfile;
using Replate.Application.Features.VendorProfiles.Commands.UpdateVendorProfile;
using Replate.Application.Features.VendorProfiles.DTOs;
using Replate.Application.Features.VendorProfiles.Queries.GetVendorProfile;
using Replate.Application.Features.VendorProfiles.Queries.GetVendorProfileByUserId;

namespace Replate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class VendorProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VendorProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new vendor profile
        /// </summary>
        /// <param name="userId">The user ID (temporary - will come from JWT later)</param>
        /// <param name="dto">Vendor profile details</param>
        /// <returns>The created vendor profile</returns>
        [HttpPost]
        [ProducesResponseType(typeof(VendorProfileDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVendorProfile(
            [FromQuery] int userId,
            [FromBody] CreateVendorProfileDto dto)
        {
            var command = new CreateVendorProfileCommand
            {
                UserId = userId,
                VendorProfile = dto
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                // ✅ Fixed: Check for null before calling .Any()
                if (result.ValidationErrors != null && result.ValidationErrors.Any())
                {
                    return BadRequest(new { errors = result.ValidationErrors });
                }
                return BadRequest(new { error = result.ErrorMessage });
            }

            return CreatedAtAction(
                nameof(GetVendorProfile),
                new { publicId = result.Data!.PublicId },
                result.Data);
        }

        /// <summary>
        /// Get vendor profile by public ID
        /// </summary>
        /// <param name="publicId">The vendor profile's public ID</param>
        /// <returns>The vendor profile</returns>
        [HttpGet("{publicId:guid}")]
        [ProducesResponseType(typeof(VendorProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVendorProfile(Guid publicId)
        {
            var query = new GetVendorProfileQuery { PublicId = publicId };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(new { error = result.ErrorMessage });
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Get vendor profile by user ID
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <returns>The vendor profile for the specified user</returns>
        [HttpGet("user/{userId:int}")]
        [ProducesResponseType(typeof(VendorProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVendorProfileByUserId(int userId)
        {
            var query = new GetVendorProfileByUserIdQuery { UserId = userId };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(new { error = result.ErrorMessage });
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Update an existing vendor profile
        /// </summary>
        /// <param name="publicId">The vendor profile's public ID</param>
        /// <param name="dto">Updated vendor profile details</param>
        /// <returns>The updated vendor profile</returns>
        [HttpPut("{publicId:guid}")]
        [ProducesResponseType(typeof(VendorProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVendorProfile(
            Guid publicId,
            [FromBody] UpdateVendorProfileDto dto)
        {
            var command = new UpdateVendorProfileCommand
            {
                PublicId = publicId,
                VendorProfile = dto
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                
                if (result.ValidationErrors != null && result.ValidationErrors.Any())
                {
                    return BadRequest(new { errors = result.ValidationErrors });
                }
                return BadRequest(new { error = result.ErrorMessage });
            }

            return Ok(result.Data);
        }
    }
}