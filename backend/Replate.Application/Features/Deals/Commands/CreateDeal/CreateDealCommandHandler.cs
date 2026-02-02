using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;
using Replate.Application.Interface;
using Replate.Domain.Entities;

namespace Replate.Application.Features.Deals.Commands.CreateDeal;

public class CreateDealCommandHandler : IRequestHandler<CreateDealCommand, Result<DealDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateDealCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<Result<DealDto>> Handle(CreateDealCommand request, CancellationToken cancellationToken)
    {
        var vendorExists = await _db.VendorProfiles
            .AnyAsync(v => v.Id == request.CreateDealDto.VendorProfileId, cancellationToken);

        if (!vendorExists)
        {
            return Result<DealDto>.Failure("Vendor profile does not exist.");
        }
        //map  the dto to the entity
        var dealEntity = _mapper.Map<Deal>(request.CreateDealDto);
        
        //add the entity to the db
        _db.Deals.Add(dealEntity);
        await _db.SaveChangesAsync(cancellationToken);
        
        var dealDto = _mapper.Map<DealDto>(dealEntity);
        
        return Result<DealDto>.Success(dealDto);
    }
}