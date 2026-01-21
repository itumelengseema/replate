namespace Replate.Domain.Entities;

public class VendorAddress
{
    public int Id { get; set; }
    public Guid PublicId { get; set; }
    
    //Address fields
    public required string Street { get; set; } 
    public string Building { get; set; } = string.Empty;
    public required string City { get; set; } = null!;
    public required string  Province { get; set; } 
    public required string PostalCode { get; set; } = null!;
    public string Country { get; set; } = "South Africa";
    
    //Geo
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    
    //Google reference 
    public string? GooglePlaceId { get; set; }
    
    
    //FK
    public int VendorProfileId { get; set; }
    public VendorProfile? VendorProfile { get; set; } 
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

}