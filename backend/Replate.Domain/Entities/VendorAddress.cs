namespace Replate.Domain.Entities;

public class VendorAddress
{
    public int Id { get; set; }
    public string Street { get; set; } = null!;
    public string Building { get; set; } = string.Empty;
    public string City { get; set; } = null!;
    public string  Province { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Country { get; set; } = "South Africa";
    
    //Geo
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    //Google reference 
    public string? GooglePlaceId { get; set; }
    
    
    //FK
    public int VendorProfileId { get; set; }
    public VendorProfile VendorProfile { get; set; } = null!;

}