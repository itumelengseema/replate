using AutoMapper;
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
        // Check if Deal Exists
        
        var dealExists = _db.Deals
            .AnyAsync(d => d.PublicId == request.PublicId, cancellationToken);

        if (!await dealExists)
        {
            return Result<DealDto>.Failure("Deal not found");
        }
        
        // Delete Deal
        var dealToDelete = await _db.Deals
            .FirstOrDefaultAsync(d => d.PublicId == request.PublicId, cancellationToken);
        
        _db.Deals.Remove(dealToDelete!);
        await _db.SaveChangesAsync(cancellationToken);
        var result = _mapper.Map<DealDto>(dealToDelete);
        return Result<DealDto>.Success(result);
     
    }
}