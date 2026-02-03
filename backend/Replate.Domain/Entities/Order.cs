using Replate.Domain.Enums;

namespace Replate.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();

        // Order Information
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string? Notes { get; set; }

        // Pickup Information
        public DateTime? PickupTime { get; set; }

        // Relationships
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int FoodListingId { get; set; }
        public FoodListing FoodListing { get; set; } = null!;

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
