namespace Replate.Application.Features.VendorProfiles.DTOs;

public class UpdateVendorProfileDto
{
    public string? BusinessName { get; set; }
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? BusinessHours { get; set; }  
    
    // Address information 
    public string? Street { get; set; }
    public string? Building { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}