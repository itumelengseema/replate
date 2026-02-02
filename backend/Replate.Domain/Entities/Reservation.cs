using Replate.Domain.Enums;

namespace Replate.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
        
        
        // Reservation Details
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public ReservationStatus Status { get; set; }
        
        //Pickup Details
        public DateTime PickupTime { get; set; }
        public string? PickupInstructions { get; set; }

        // Which deal is reserved
        public int DealId { get; set; }
        public Deal Deal { get; set; } = null!;

        // Which user reserved it
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
  
    }
}
