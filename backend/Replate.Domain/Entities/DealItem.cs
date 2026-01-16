namespace Replate.Domain.Entities
{
    public class DealItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int Quantity { get; set; }
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        // Foreign Key to Deal
        public int DealId { get; set; }
        public Deal Deal { get; set; } = null!;
    }
}
