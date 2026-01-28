﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.VendorProfiles.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.VendorProfiles.Queries.GetVendorProfileByUserId;

public class GetVendorProfileByUserIdQueryHandler 
   : IRequestHandler<GetVendorProfileByUserIdQuery, Result<VendorProfileDto>>

{
   private readonly IApplicationDbContext _db;
   private readonly IMapper _mapper;

   public GetVendorProfileByUserIdQueryHandler(IApplicationDbContext db, IMapper mapper)
   {
      _db = db;
      _mapper = mapper;
   }
   
   public async Task<Result<VendorProfileDto>> Handle(GetVendorProfileByUserIdQuery request, CancellationToken cancellationToken)
   {
      // find vendor profile by user id
      var vendorProfile = await _db.VendorProfiles
         .Include(v=> v.VendorAddress)
         .FirstOrDefaultAsync(vp => vp.UserId == request.UserId, cancellationToken);

      if (vendorProfile == null)
      {
         return Result<VendorProfileDto>.Failure("Vendor profile not found for this user");
      }

      var results = _mapper.Map<VendorProfileDto>(vendorProfile);
      return Result<VendorProfileDto>.Success(results);
   }
}