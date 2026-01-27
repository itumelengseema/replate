using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.VendorProfiles.DTOs;

namespace Replate.Application.Features.VendorProfiles.Commands.CreateVendorProfile;

public class CreateVendorProfileCommand : IRequest<Result<VendorProfileDto>>
{
    public int UserId { get; set; }
    public CreateVendorProfileDto VendorProfile { get; set; } = null!;
  
}