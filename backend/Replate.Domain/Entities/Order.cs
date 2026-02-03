using Replate.Domain.Enums;

namespace Replate.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
        
        
        // Order Details
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        
        //Pickup Details
        public DateTime PickupTime { get; set; }
        public string? PickupInstructions { get; set; }

        // Which food listing is ordered
        public int FoodListingId { get; set; }
        public FoodListing FoodListing { get; set; } = null!;

        // Which user ordered it
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
