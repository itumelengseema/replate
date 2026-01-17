namespace Replate.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();


        public string FirebaseUid { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;




        public IEnumerable<VendorProfile>? VendorProfile { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }

        public bool IsVendor => VendorProfile != null;
    }
}
