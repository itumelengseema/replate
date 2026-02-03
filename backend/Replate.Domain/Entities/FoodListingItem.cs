namespace Replate.Domain.Entities
{
    public class FoodListingItem
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; } = string.Empty;

        // Foreign Key to FoodListing
        public int FoodListingId { get; set; }
        public FoodListing FoodListing { get; set; } = null!;
    }
}
