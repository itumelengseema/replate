using FluentValidation;

namespace Replate.Application.Features.FoodListings.Commands.CreateFoodListing;

public class CreateFoodListingCommandValidator : AbstractValidator<CreateFoodListingCommand>
{
    public CreateFoodListingCommandValidator()
    {
        RuleFor(x => x.VendorProfileId)
            .GreaterThan(0).WithMessage("Vendor profile ID is required");

        RuleFor(x => x.FoodListing.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

        RuleFor(x => x.FoodListing.OriginalPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Original price must be non-negative");

        RuleFor(x => x.FoodListing.DiscountedPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Discounted price must be non-negative")
            .LessThanOrEqualTo(x => x.FoodListing.OriginalPrice)
            .WithMessage("Discounted price must be less than or equal to original price");

        RuleFor(x => x.FoodListing.QuantityAvailable)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity must be non-negative");

        RuleFor(x => x.FoodListing.AvailableUntil)
            .GreaterThan(x => x.FoodListing.AvailableFrom)
            .WithMessage("Available until must be after available from");
    }
}
