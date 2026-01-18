namespace Replate.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        
        public Guid PublicId { get; set; } = Guid.NewGuid();

        // Which deal is reserved
        public int DealId { get; set; }
        public Deal Deal { get; set; } = null!;

        // Which user reserved it
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
    }
}
