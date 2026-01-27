namespace Replate.Application.Features.VendorProfiles.DTOs;

public class VendorAddressDto
{
  
    public Guid PublicId { get; set; }
    
    //Address fields
    public string Street { get; set; } = string.Empty;
    public string Building { get; set; } = string.Empty;
    public string City { get; set; }  = string.Empty;
    public string  Province { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    
    //Geo
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    
 
    
}