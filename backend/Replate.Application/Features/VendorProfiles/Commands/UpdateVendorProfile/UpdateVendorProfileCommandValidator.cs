using FluentValidation;

namespace Replate.Application.Features.VendorProfiles.Commands.UpdateVendorProfile;

public class UpdateVendorProfileCommandValidator :  AbstractValidator<UpdateVendorProfileCommand>
{
    public UpdateVendorProfileCommandValidator()
    {
        RuleFor(x => x.PublicId)
                .NotEmpty()
                .WithMessage("Vendor profile ID is required.");

            When(x => !string.IsNullOrWhiteSpace(x.VendorProfile.BusinessName), () =>
            {
                RuleFor(x => x.VendorProfile.BusinessName)
                    .MaximumLength(200)
                    .WithMessage("Business name cannot exceed 200 characters.");
            });
            
            RuleFor(x => x.VendorProfile.Building)
                .MaximumLength(100)
                .WithMessage("Building cannot exceed 100 characters.");
                

            When(x => !string.IsNullOrWhiteSpace(x.VendorProfile.PhoneNumber), () =>
            {
                RuleFor(x => x.VendorProfile.PhoneNumber)
                    .MaximumLength(20)
                    .WithMessage("Phone number cannot exceed 20 characters.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.VendorProfile.Email), () =>
            {
                RuleFor(x => x.VendorProfile.Email)
                    .EmailAddress()
                    .WithMessage("Invalid email format.")
                    .MaximumLength(100)
                    .WithMessage("Email cannot exceed 100 characters.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.VendorProfile.Street), () =>
            {
                RuleFor(x => x.VendorProfile.Street)
                    .MaximumLength(200)
                    .WithMessage("Street cannot exceed 200 characters.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.VendorProfile.City), () =>
            {
                RuleFor(x => x.VendorProfile.City)
                    .MaximumLength(100)
                    .WithMessage("City cannot exceed 100 characters.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.VendorProfile.Province), () =>
            {
                RuleFor(x => x.VendorProfile.Province)
                    .MaximumLength(100)
                    .WithMessage("Province cannot exceed 100 characters.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.VendorProfile.PostalCode), () =>
            {
                RuleFor(x => x.VendorProfile.PostalCode)
                    .MaximumLength(20)
                    .WithMessage("Postal code cannot exceed 20 characters.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.VendorProfile.Country), () =>
            {
                RuleFor(x => x.VendorProfile.Country)
                    .MaximumLength(100)
                    .WithMessage("Country cannot exceed 100 characters.");
            });
    }
}