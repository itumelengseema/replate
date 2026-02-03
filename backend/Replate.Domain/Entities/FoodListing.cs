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
        public int AvailableQuantity { get; set; }
        
        // Food Listing Type & Category
        public FoodListingType FoodListingType { get; set; }
        public FoodCategory Category { get; set; }
        

        // Timing
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableUntil { get; set; }

        
        // Vendor who posted the food listing
        public int VendorProfileId { get; set; }
        public VendorProfile VendorProfile { get; set; } = null!;


        //Items in the food listing 1 or many

        public ICollection<FoodListingItem> FoodListingItems { get; set; } = new List<FoodListingItem>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        
    }
}
