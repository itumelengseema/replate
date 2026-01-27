

namespace Replate.Application.Features.VendorProfiles.DTOs;

public class VendorProfileDto
{

    public Guid PublicId { get; set; } = Guid.NewGuid();
        
 
    public string BusinessName { get; set; } = string.Empty;
    public string?  Description { get; set; }
    public string? LogoUrl { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public required string Email { get; set; } = string.Empty;
        

    public string? BusinessHours { get; set; }
    public bool IsActive { get; set; } = true;    
    

        

    public VendorAddressDto?  Address { get; set; }
        

    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime. UtcNow;
    public DateTime?  UpdatedAt { get; set; }

}