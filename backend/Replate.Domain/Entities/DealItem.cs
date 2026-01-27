namespace Replate.Domain.Entities
{
    public class DealItem
    {
        public int Id { get; set; }
        
        
        public required string Name { get; set; } 
        public int Quantity { get; set; }
        public string Description { get; set; } = string.Empty;


        // Foreign Key to Deal
        public int DealId { get; set; }
        public Deal Deal { get; set; } = null!;
    }
}
