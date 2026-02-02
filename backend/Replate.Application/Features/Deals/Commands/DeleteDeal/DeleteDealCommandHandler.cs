﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;
using Replate.Application.Interface;
using Replate.Domain.Entities;

namespace Replate.Application.Features.Deals.Commands.DeleteDeal;

public class DeleteDealCommandHandler : IRequestHandler<DeleteDealCommand, Result<DealDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public DeleteDealCommandHandler( IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<Result<DealDto>> Handle(DeleteDealCommand request, CancellationToken cancellationToken)
    {
        // Check if Food Listing Exists
        
        var foodListingExists = _db.FoodListings
            .AnyAsync(d => d.PublicId == request.PublicId, cancellationToken);

        if (!await foodListingExists)
        {
            return Result<DealDto>.Failure("Deal not found");
        }
        
        // Delete Food Listing
        var foodListingToDelete = await _db.FoodListings
            .FirstOrDefaultAsync(d => d.PublicId == request.PublicId, cancellationToken);
        
        _db.FoodListings.Remove(foodListingToDelete!);
        await _db.SaveChangesAsync(cancellationToken);
        var result = _mapper.Map<DealDto>(foodListingToDelete);
        return Result<DealDto>.Success(result);
    }
}