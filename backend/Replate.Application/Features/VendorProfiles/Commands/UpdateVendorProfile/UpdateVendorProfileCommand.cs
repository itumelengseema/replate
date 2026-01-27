using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.VendorProfiles.DTOs;

namespace Replate.Application.Features.VendorProfiles.Commands.UpdateVendorProfile;

public class UpdateVendorProfileCommand : IRequest<Result<VendorProfileDto>>
{
    public Guid PublicId { get; set; }
    public UpdateVendorProfileDto VendorProfile { get; set; } = null!;
}