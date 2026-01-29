using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;
using Replate.Application.Interface;


namespace Replate.Application.Features.Deals.Commands.UpdateDeal;



public class UpdateDealCommandHandler :
    IRequestHandler<UpdateDealCommand, Result<DealDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UpdateDealCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<DealDto>> Handle(UpdateDealCommand request, CancellationToken cancellationToken)
    {
        // Find the deal by public ID
        var deal = await _db.Deals
            .FirstOrDefaultAsync(d => d.PublicId == request.DealId, cancellationToken);

        if (deal == null)
        {
            return Result<DealDto>.Failure("Deal not found");
        }
        
        //Update the deal properties
        if (!string.IsNullOrWhiteSpace(request.UpdateDealDto.Title))
            deal.Title = request.UpdateDealDto.Title;
        if (!string.IsNullOrWhiteSpace(request.UpdateDealDto.Description))
            deal.Description = request.UpdateDealDto.Description;
        if (request.UpdateDealDto.OriginalPrice.HasValue)
            deal.OriginalPrice = request.UpdateDealDto.OriginalPrice.Value;
        if (request.UpdateDealDto.DiscountedPrice.HasValue)
            deal.DiscountedPrice = request.UpdateDealDto.DiscountedPrice.Value;
        if (request.UpdateDealDto.AvailableQuantity.HasValue)
            deal.AvailableQuantity = request.UpdateDealDto.AvailableQuantity.Value;
        if (request.UpdateDealDto.DealType.HasValue)
            deal.DealType = request.UpdateDealDto.DealType.Value;
        if (request.UpdateDealDto.Category.HasValue)
            deal.Category = request.UpdateDealDto.Category.Value;
        if (request.UpdateDealDto.AvailableFrom.HasValue)
            deal.AvailableFrom = request.UpdateDealDto.AvailableFrom.Value;
        if (request.UpdateDealDto.AvailableUntil.HasValue)
            deal.AvailableUntil = request.UpdateDealDto.AvailableUntil.Value;
        if (request.UpdateDealDto.VendorProfileId.HasValue)
            deal.VendorProfileId = request.UpdateDealDto.VendorProfileId.Value;
       
        //Update the UpdatedAt timestamp
        deal.UpdatedAt = DateTime.UtcNow;
        
        // Save changes to the database
        await _db.SaveChangesAsync(cancellationToken);
        // Map to DTO and return success result
        var dealDto = _mapper.Map<DealDto>(deal);
        return Result<DealDto>.Success(dealDto);
            
    }


}
