using AutoMapper;
using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;
using Replate.Application.Interface;
using Replate.Domain.Entities;

namespace Replate.Application.Features.Deals.Commands;

public class CreateDealCommandHandler : IRequestHandler<CreateDealDto, Result<DealDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateDealCommandHandler( IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<Result<DealDto>> Handle(CreateDealDto request, CancellationToken cancellationToken)
    {
        //map  the dto to the entity
        var dealEntity = _mapper.Map<Deal>(request);
        
        //add the entity to the db
        _db.Deals.Add(dealEntity);
        await _db.SaveChangesAsync(cancellationToken);
        
        var dealDto = _mapper.Map<DealDto>(dealEntity);
        
        return Result<DealDto>.Success(dealDto);
    }
}