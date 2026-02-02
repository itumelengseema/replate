﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.Deals.Queries.GetDealByIdQuery;

public class GetDealByIdQueryHandler : IRequestHandler<GetDealByIdQuery, Result<DealDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;
    public GetDealByIdQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<Result<DealDto>> Handle(GetDealByIdQuery request, CancellationToken cancellationToken)
    {
     var foodListing = await _db.FoodListings
         .FirstOrDefaultAsync(d=>d.PublicId == request.Id, cancellationToken);
     
     var result = _mapper.Map<DealDto>(foodListing);
     return Result<DealDto>.Success(result);
    }
}
