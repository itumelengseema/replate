namespace Replate.Application.Features.FoodListingItem.Dtos;

public class FoodListingItemDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = string.Empty;
    public int FoodListingId { get; set; }
}
