using AutoMapper;
using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;
using Replate.Application.Interface;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Replate.Application.Features.Deals.Queries.GetAllDealsQuery;

public class GetAllDealsQueryHandler : IRequestHandler<GetAllDealsQuery, Result<List<DealDto>>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;
    
    public GetAllDealsQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<Result<List<DealDto>>> Handle(GetAllDealsQuery request, CancellationToken cancellationToken)
    {
        // get all deals List from database
        var deals = await _db.Deals.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<DealDto>>(deals);
        return Result<List<DealDto>>.Success(result);
    }
}