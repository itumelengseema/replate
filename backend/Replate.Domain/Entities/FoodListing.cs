using Replate.Domain.Enums;

namespace Replate.Domain.Entities
{
    public class FoodListing
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();

        // Food Listing Information
        public required string Title { get; set; }
        public string? Description { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int QuantityAvailable { get; set; }

        // Categorization
        public FoodCategory Category { get; set; } = FoodCategory.Other;
        public FoodListingType ListingType { get; set; } = FoodListingType.Discount;

        // Availability
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableUntil { get; set; }
        public string? ImageUrl { get; set; }

        // Relationships
        public int VendorProfileId { get; set; }
        public VendorProfile VendorProfile { get; set; } = null!;

        public ICollection<FoodListingItem> Items { get; set; } = new List<FoodListingItem>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
