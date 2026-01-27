using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.VendorProfiles.DTOs;

namespace Replate.Application.Features.VendorProfiles.Queries.GetVendorProfile;

public class GetVendorProfileQuery : IRequest<Result<VendorProfileDto>>
{
    public Guid PublicId { get; set; }
}