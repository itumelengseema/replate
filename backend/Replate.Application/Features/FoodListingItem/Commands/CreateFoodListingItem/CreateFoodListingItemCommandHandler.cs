using AutoMapper;
using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListingItem.Dtos;
using Replate.Application.Interface;

using FoodListingItemEntity = Replate.Domain.Entities.FoodListingItem;


namespace Replate.Application.Features.FoodListingItem.Commands.CreateFoodListingItem;

public class CreateFoodListingItemCommandHandler : IRequestHandler<CreateFoodListingItemCommand, Result<FoodListingItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateFoodListingItemCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<FoodListingItemDto>> Handle(CreateFoodListingItemCommand request, CancellationToken cancellationToken)
    {
        var foodListingItem = new FoodListingItemEntity
        {
            Name = request.Name,
            Quantity = request.Quantity,
            Description = request.Description,
            FoodListingId = request.FoodListingId
        };

        _context.FoodListingItems.Add(foodListingItem);
        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<FoodListingItemDto>(foodListingItem);
        return Result<FoodListingItemDto>.Success(result);
    }
}
