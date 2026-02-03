using FluentValidation;

namespace Replate.Application.Features.FoodListingItems.Commands.CreateFoodListingItem;

public class CreateFoodListingItemCommandValidator : AbstractValidator<CreateFoodListingItemCommand>
{
    public CreateFoodListingItemCommandValidator()
    {
        RuleFor(x => x.FoodListingItem.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.FoodListingItem.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity must be non-negative");

        RuleFor(x => x.FoodListingItem.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.FoodListingItem.FoodListingPublicId)
            .NotEmpty().WithMessage("Food listing ID is required");
    }
}
