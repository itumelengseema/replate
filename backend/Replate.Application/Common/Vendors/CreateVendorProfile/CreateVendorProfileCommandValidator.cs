using FluentValidation;
using FluentValidation.Validators;

namespace Replate.Application.Common.Vendors.CreateVendorProfile;

public class CreateVendorProfileCommandValidator : AbstractValidator<CreateVendorProfileCommand>
{
    public CreateVendorProfileCommandValidator()
    {
        RuleFor( x => x.UserId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("UserId must be greater or equal 0");
        
        RuleFor(x => x.BusinessName)
            .NotEmpty()
            .WithMessage("BusinessName cannot be empty")
            .MaximumLength(200)
            .WithMessage("BusinessName cannot exceed 200 characters");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description cannot be empty")
            .MaximumLength(1000)
            .WithMessage("Description cannot exceed 1000 characters");
        
        RuleFor(x=> x.PhoneNumber) 
            .NotEmpty()
            .WithMessage("Phone Number cannot be empty")
            .Matches(@"\+?[1-9]\d{1,14}$")
            .WithMessage("Phone number must be in valid international format (e.g., +27123456789).");

        RuleFor(x => x.Address)
            .NotNull()
            .WithMessage("Address is required")
            .SetValidator(new CreateVendorAddressDtoValidator());
        
        RuleFor(x => x.LogoUrl)
            .Must(BeAValidUrl)
            .WithMessage("LogoUrl must be a valid url")
            .When(x => !string.IsNullOrEmpty(x.LogoUrl))
            .WithMessage("LogoUrl must be a valid url");
        
        RuleFor(x =>x.BannerImageUrl)
            .Must(BeAValidUrl)
            .WithMessage("BannerImageUrl must be a valid url")
            .When(x => !string.IsNullOrEmpty(x.BannerImageUrl))
            .WithMessage("BannerImageUrl must be a valid url");
        
        
           





    }

    private bool BeAValidUrl(string? url)
    {
        if(string.IsNullOrEmpty(url)) return true;
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) 
            &&( uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

}

public class CreateVendorAddressDtoValidator : AbstractValidator<CreateVendorAddressDto>
{
    public CreateVendorAddressDtoValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Street Name cannot be empty")
            .MaximumLength(200)
            .WithMessage("Street Name cannot exceed 200 characters");
        
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City required")
            .MaximumLength(100)
            .WithMessage("City cannot exceed 100 characters");
        
        RuleFor(x => x.Province)
            .NotEmpty()
            .WithMessage("Province required")
            .MaximumLength(100)
            .WithMessage("Province cannot exceed 100 characters");
        
        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .WithMessage("Postal Code required")
            .MaximumLength(10)
            .WithMessage("Postal Code cannot exceed 10 characters");
        
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90 degrees");
        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180 degrees");
    }
}