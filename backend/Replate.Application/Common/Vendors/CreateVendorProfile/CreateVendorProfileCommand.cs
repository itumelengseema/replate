namespace Replate.Application.Common.Vendors.CreateVendorProfile;

public class CreateVendorProfileCommand
{
   public int UserId { get; init; }
   public string BusinessName { get; init; } = null!;
   public string Description { get; init; } = null!;
   public string PhoneNumber { get; init; } = null!;
   public  CreateVendorAddressDto Address { get; init; } = null!;
   public string? LogoUrl { get; init; }
   public string? BannerImageUrl { get; init; }
}

public record CreateVendorAddressDto
{
   public string Street { get; init; } = null!;
   public string Building { get; init; } = string.Empty;
   public string City { get; init; } = null!;
   public string Province { get; init; } = null!;
   public string PostalCode { get; init; } = null!;
   public string Country { get; init; } = "South Africa";
   public double Latitude { get; init; }
   public double Longitude { get; init; }
   public string? GooglePlaceId { get; init; }
}