using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.VendorProfiles.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.VendorProfiles.Queries.GetVendorProfile;

public class GetVendorProfileQueryHandler(IApplicationDbContext db, IMapper mapper)
    : IRequestHandler<GetVendorProfileQuery, Result<VendorProfileDto>>
{
    public async Task<Result<VendorProfileDto>> Handle(GetVendorProfileQuery request, CancellationToken cancellationToken)
    {
        // find vendor profile by public id
      var vendorProfile =  await db.VendorProfiles
          .Include(v => v.VendorAddress)
            .FirstOrDefaultAsync(vp => vp.PublicId == request.PublicId, cancellationToken);
        
      // check if vendor profile exists
      if (vendorProfile == null)
      {
          return Result<VendorProfileDto>.Failure("Vendor profile not found");
      }
      
      // map to dto
      var results = mapper.Map<VendorProfileDto>(vendorProfile);
      // return success result
        return Result<VendorProfileDto>.Success(results);
    }
}