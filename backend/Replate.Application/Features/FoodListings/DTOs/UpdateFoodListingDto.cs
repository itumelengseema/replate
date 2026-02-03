using Replate.Domain.Enums;

namespace Replate.Application.Features.FoodListings.DTOs;

public class UpdateFoodListingDto
{
    public Guid PublicId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? OriginalPrice { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public int? AvailableQuantity { get; set; }
    public FoodListingType? FoodListingType { get; set; }
    public FoodCategory? Category { get; set; }
    public DateTime? AvailableFrom { get; set; }
    public DateTime? AvailableUntil { get; set; }
    public int? VendorProfileId { get; set; }
    public List<int>? FoodListingItemIds { get; set; }
}
