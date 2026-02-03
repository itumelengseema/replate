namespace Replate.Application.Features.FoodListingItems.DTOs;

public class UpdateFoodListingItemDto
{
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = string.Empty;
}
