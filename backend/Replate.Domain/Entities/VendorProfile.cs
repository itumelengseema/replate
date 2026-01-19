namespace Replate.Domain.Entities
{
    public class VendorProfile
    {
        public int Id { get; set; }

        public Guid PublicId { get; set; } = Guid.NewGuid();

        //Business details
        public string BusinessName { get; set; } = null!;
        public string Decscription { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        
        //Address
        public VendorAddress Address { get; set; } = null!;

        //Branding
        public string? LogoImageUrl { get; set; }
        public string? BannerImageUrl { get; set; }

        // Ownership 
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        //Deals owned by vendor
        public ICollection<Deal>? Deals { get; set; }

    }
}
