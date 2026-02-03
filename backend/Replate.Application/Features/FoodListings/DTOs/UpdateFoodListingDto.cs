using Replate.Domain.Enums;

namespace Replate.Application.Features.FoodListings.DTOs;

public class UpdateFoodListingDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountedPrice { get; set; }
    public int QuantityAvailable { get; set; }
    public FoodCategory Category { get; set; }
    public FoodListingType ListingType { get; set; }
    public DateTime AvailableFrom { get; set; }
    public DateTime AvailableUntil { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
}
