using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;
using Replate.Application.Interface;
using Replate.Domain.Entities;

namespace Replate.Application.Features.FoodListings.Commands.CreateFoodListing;

public class CreateFoodListingCommandHandler : IRequestHandler<CreateFoodListingCommand, Result<FoodListingDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateFoodListingCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<Result<FoodListingDto>> Handle(CreateFoodListingCommand request, CancellationToken cancellationToken)
    {
        var vendorExists = await _db.VendorProfiles
            .AnyAsync(v => v.Id == request.CreateFoodListingDto.VendorProfileId, cancellationToken);

        if (!vendorExists)
        {
            return Result<FoodListingDto>.Failure("Vendor profile does not exist.");
        }
        //map  the dto to the entity
        var foodListingEntity = _mapper.Map<FoodListing>(request.CreateFoodListingDto);
        
        //add the entity to the db
        _db.FoodListings.Add(foodListingEntity);
        await _db.SaveChangesAsync(cancellationToken);
        
        var foodListingDto = _mapper.Map<FoodListingDto>(foodListingEntity);
        
        return Result<FoodListingDto>.Success(foodListingDto);
    }
}
