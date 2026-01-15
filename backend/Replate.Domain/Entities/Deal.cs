namespace Replate.Domain.Entities
{
    public class Deal
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!; // e.g., "Food", "Beverage"


        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }


        public DateTime ExpiryDate { get; set; }
        public bool IsClaimed { get; set; }

        // Vendor who posted the deal
        public int VendorId { get; set; }
        public User Vendor { get; set; } = null!;


        // Navigation: Reservations made by customers
        public ICollection<Reservation>? Reservations { get; set; }

    }
}
