using Replate.Domain.Enums;

namespace Replate.Application.Features.FoodListings.DTOs;

public class FoodListingDto
{
    public Guid PublicId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountedPrice { get; set; }
    public int QuantityAvailable { get; set; }
    public FoodCategory Category { get; set; }
    public FoodListingType ListingType { get; set; }
    public DateTime AvailableFrom { get; set; }
    public DateTime AvailableUntil { get; set; }
    public string? ImageUrl { get; set; }
    public Guid VendorPublicId { get; set; }
    public string VendorName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<FoodListingItemDto> Items { get; set; } = new();
}
