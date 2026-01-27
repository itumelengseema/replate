using FluentValidation;

namespace Replate.Application.Features.VendorProfiles.Commands.CreateVendorProfile;

public class CreateVendorProfileCommandValidator : AbstractValidator<CreateVendorProfileCommand>
{
    public CreateVendorProfileCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be a positive integer.");
        
        RuleFor(x => x.VendorProfile.BusinessName)
            .NotEmpty()
            .WithMessage(" Business Name is required.")
            .MaximumLength(200)
            .WithMessage(" Business Name cannot exceed 200 characters.");
        
        RuleFor(x => x.VendorProfile.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone Number is required.")
            .MaximumLength(10)
            .WithMessage("Phone Number cannot exceed 10 characters.");
        
        RuleFor(x => x.VendorProfile.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(100)
            .WithMessage("Email cannot exceed 100 characters.");
        
        RuleFor(x => x.VendorProfile.Street)
            .NotEmpty()
            .WithMessage("Street is required.")
            .MaximumLength(100)
            .WithMessage("Street cannot exceed 100 characters.");
        
        RuleFor(x => x.VendorProfile.Building)
            .MaximumLength(50)
            .WithMessage("Building cannot exceed 50 characters.");
        
        RuleFor(x => x.VendorProfile.City)
            .NotEmpty()
            .WithMessage("City is required.")
            .MaximumLength(100)
            .WithMessage("City cannot exceed 100 characters.");
        
       RuleFor(x => x.VendorProfile.City)
           .NotEmpty()
           .WithMessage("City is required.")
           .MaximumLength(100)
           .WithMessage("City cannot exceed 100 characters.");
       
       RuleFor(x => x.VendorProfile.Province)
           .NotEmpty()
           .WithMessage("Province is required.")
           .MaximumLength(100)
           .WithMessage("Province cannot exceed 100 characters.");
       
       RuleFor(x => x.VendorProfile.PostalCode)
           .NotEmpty()
           .WithMessage("Postal Code is required.")
           .MaximumLength(5)
           .WithMessage("Postal Code cannot exceed 5 characters.");
       
         RuleFor(x => x.VendorProfile.Country)
             .NotEmpty()
             .WithMessage("Country is required.")
             .MaximumLength(100)
             .WithMessage("Country cannot exceed 100 characters.");
    }
}