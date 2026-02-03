namespace Replate.Application.Features.FoodListingItems.DTOs;

public class FoodListingItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Description { get; set; } = string.Empty;
    public int FoodListingId { get; set; }
    public Guid FoodListingPublicId { get; set; }
}
