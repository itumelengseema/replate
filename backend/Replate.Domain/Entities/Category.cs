namespace Replate.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string DefaultImageUrl { get; set; } = null!;

        public ICollection<Deal>? Deals { get; set; }
    }
}
