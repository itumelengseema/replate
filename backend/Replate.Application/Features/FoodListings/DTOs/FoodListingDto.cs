using Replate.Domain.Enums;

namespace Replate.Application.Features.FoodListings.DTOs;

public class FoodListingDto
{
    public Guid PublicId { get; set; }
    // ...existing code...
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal OriginalPrice { get; set; }
    public decimal DiscountedPrice { get; set; }
    public int AvailableQuantity { get; set; }
    public FoodListingType FoodListingType { get; set; }
    public FoodCategory Category { get; set; }
    public DateTime AvailableFrom { get; set; }
    public DateTime AvailableUntil { get; set; }
    public int VendorProfileId { get; set; }
    public List<int> FoodListingItemIds { get; set; } = new();
    public List<int> ReservationIds { get; set; } = new(); 
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}
