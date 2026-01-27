using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.VendorProfiles.DTOs;

namespace Replate.Application.Features.VendorProfiles.Queries.GetVendorProfileByUserId;

public class GetVendorProfileByUserIdQuery : IRequest<Result<VendorProfileDto>>
{
    public int UserId { get; set; }
}