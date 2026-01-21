namespace Replate.Application.DTOs;

public record VendorProfileDto
{
    public Guid Id { get; init; }
    public string BusinessName { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public string VendorAddressDto { get; init; } = null!;
    public string? LogoUrl { get; init; }
    public string? BannerImageUrl { get; init; }
}

public record VendorAddressDto
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