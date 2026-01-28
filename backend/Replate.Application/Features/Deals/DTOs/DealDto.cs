using Replate.Domain.Enums;

namespace Replate.Application.Features.Deals.DTOs;

public class DealDto
{
    public Guid PublicId { get; set; }
        
    // Deal Information
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
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
 
    
    //Items in the deal 1 or many
    public List<int> DealItemIds { get; set; } = new();


    public List<int> ReservationIds { get; set; } = new(); 

    // Audit fields
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

}