using Replate.Domain.Enums;

namespace Replate.Domain.Entities
{
    public class Deal
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;


        public FoodCategory Category { get; set; }

        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }


        public DateTime ExpiryDate { get; set; }


        public int QuantityAvailable { get; set; }

        public bool IsActive { get; set; } = true;



        // Vendor who posted the deal
        public int VendorProfileId { get; set; }


        //Items in the deal 1 or many

        public ICollection<DealItem>? Items { get; set; } = new List<DealItem>();
        public ICollection<Reservation>? Reservations { get; set; }




    }
}
