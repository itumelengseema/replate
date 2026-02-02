using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.Deals.Commands.PatchDeal;

public class PatchDealCommandHandler 
    : IRequestHandler<PatchDealCommand, Result<DealDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;
       
    public PatchDealCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<Result<DealDto>> Handle(PatchDealCommand request, CancellationToken cancellationToken)
    {
       var foodListing = await _db.FoodListings
           .FirstOrDefaultAsync(d => d.PublicId == request.PatchDealDto.PublicId, cancellationToken);

       if (foodListing == null)
       {
           return Result<DealDto>.Failure("Deal not found");
       }
       // Patch only the provided Fields
       //Update the food listing properties
       if (!string.IsNullOrWhiteSpace(request.PatchDealDto.Title))
           foodListing.Title = request.PatchDealDto.Title;
       if (!string.IsNullOrWhiteSpace(request.PatchDealDto.Description))
           foodListing.Description = request.PatchDealDto.Description;
       if (request.PatchDealDto.OriginalPrice.HasValue)
           foodListing.OriginalPrice = request.PatchDealDto.OriginalPrice.Value;
       if (request.PatchDealDto.DiscountedPrice.HasValue)
           foodListing.DiscountedPrice = request.PatchDealDto.DiscountedPrice.Value;
       if (request.PatchDealDto.AvailableQuantity.HasValue)
           foodListing.AvailableQuantity = request.PatchDealDto.AvailableQuantity.Value;
       if (request.PatchDealDto.FoodListingType.HasValue)
           foodListing.FoodListingType = request.PatchDealDto.FoodListingType.Value;
       if (request.PatchDealDto.Category.HasValue)
           foodListing.Category = request.PatchDealDto.Category.Value;
       if (request.PatchDealDto.AvailableFrom.HasValue)
           foodListing.AvailableFrom = request.PatchDealDto.AvailableFrom.Value;
       if (request.PatchDealDto.AvailableUntil.HasValue)
           foodListing.AvailableUntil = request.PatchDealDto.AvailableUntil.Value;
       
       foodListing.UpdatedAt = DateTime.UtcNow;
       try
       {
           await _db.SaveChangesAsync(cancellationToken);
       }
       catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
       {
           return Result<DealDto>.Failure("The update operation failed due to a foreign key constraint violation.");
       }

       return Result<DealDto>.Success(_mapper.Map<DealDto>(foodListing));
    }
}
