using Replate.Domain.Enums;

namespace Replate.Domain.Entities
{
    public class Deal
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();

        
        // Deal Information
        public required string Title { get; set; }
        public string? Description { get; set; } 
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int AvailableQuantity { get; set; }
        
        //Deal Type & Category
        public DealType DealType { get; set; }
        public FoodCategory Category { get; set; }

      


        // Timing
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableUntil { get; set; }

    

     



        // Vendor who posted the deal
        public int VendorProfileId { get; set; }
        public VendorProfile VendorProfile { get; set; } = null!;


        //Items in the deal 1 or many

        public ICollection<DealItem> DealItems { get; set; } = new List<DealItem>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;




    }
}
