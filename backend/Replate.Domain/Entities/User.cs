namespace Replate.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirebaseUid { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = "User";


        // Navigation properties
        public ICollection<Deal>? Deals { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}
