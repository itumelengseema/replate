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
       var deal = await _db.Deals
           .FirstOrDefaultAsync(d => d.PublicId == request.PatchDealDto.PublicId, cancellationToken);

       if (deal == null)
       {
           return Result<DealDto>.Failure("Deal not found");
       }
       // Patch only the provided Fields
       //Update the deal properties
       if (!string.IsNullOrWhiteSpace(request.PatchDealDto.Title))
           deal.Title = request.PatchDealDto.Title;
       if (!string.IsNullOrWhiteSpace(request.PatchDealDto.Description))
           deal.Description = request.PatchDealDto.Description;
       if (request.PatchDealDto.OriginalPrice.HasValue)
           deal.OriginalPrice = request.PatchDealDto.OriginalPrice.Value;
       if (request.PatchDealDto.DiscountedPrice.HasValue)
           deal.DiscountedPrice = request.PatchDealDto.DiscountedPrice.Value;
       if (request.PatchDealDto.AvailableQuantity.HasValue)
           deal.AvailableQuantity = request.PatchDealDto.AvailableQuantity.Value;
       if (request.PatchDealDto.DealType.HasValue)
           deal.DealType = request.PatchDealDto.DealType.Value;
       if (request.PatchDealDto.Category.HasValue)
           deal.Category = request.PatchDealDto.Category.Value;
       if (request.PatchDealDto.AvailableFrom.HasValue)
           deal.AvailableFrom = request.PatchDealDto.AvailableFrom.Value;
       if (request.PatchDealDto.AvailableUntil.HasValue)
           deal.AvailableUntil = request.PatchDealDto.AvailableUntil.Value;
       
       deal.UpdatedAt = DateTime.UtcNow;
       try
       {
           await _db.SaveChangesAsync(cancellationToken);
       }
       catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
       {
           return Result<DealDto>.Failure("The update operation failed due to a foreign key constraint violation.");
       }

       return Result<DealDto>.Success(_mapper.Map<DealDto>(deal));
    }
}
