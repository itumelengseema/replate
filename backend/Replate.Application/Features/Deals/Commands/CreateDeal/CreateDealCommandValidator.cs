using FluentValidation;

namespace Replate.Application.Features.Deals.Commands.CreateDeal;

public class CreateDealCommandValidator : AbstractValidator<CreateDealCommand>
{
    public CreateDealCommandValidator()
    {
        RuleFor(x => x.CreateDealDto)
            .NotNull()
            .WithMessage("Deal information must be provided.");

        RuleFor(x => x.CreateDealDto.Title)
            .NotEmpty()
            .WithMessage("Deal Title is required.")
            .MaximumLength(200)
            .WithMessage("Deal Title cannot exceed 200 characters.");

        RuleFor(x => x.CreateDealDto.Description)
            .NotEmpty()
            .WithMessage("Deal Description is required.")
            .MaximumLength(1000)
            .WithMessage("Deal Description cannot exceed 1000 characters.");

        RuleFor(x => x.CreateDealDto.OriginalPrice)
            .GreaterThan(0)
            .WithMessage("Deal Price must be a positive value.");

        RuleFor(x => x.CreateDealDto.DiscountedPrice)
            .GreaterThan(0)
            .WithMessage("Discounted Price must be a positive value.")
            .LessThan(x => x.CreateDealDto.OriginalPrice)
            .WithMessage("Discounted Price must be less than Original Price.");

        RuleFor(x => x.CreateDealDto.AvailableQuantity)
            .GreaterThan(0)
            .WithMessage("Available Quantity must be a positive integer.");

        RuleFor(x => x.CreateDealDto.AvailableFrom)
            .Must(BeAValidDate)
            .WithMessage("Available From must be a valid date.");

        RuleFor(x => x.CreateDealDto.AvailableUntil)
            .Must(BeAValidDate)
            .WithMessage("Available Until must be a valid date.");

        RuleFor(x => x.CreateDealDto)
            .Must(d => d.AvailableFrom < d.AvailableUntil)
            .WithMessage("Available From must be before Available Until.");

        RuleFor(x => x.CreateDealDto.Category)
            .IsInEnum()
            .WithMessage("Category must be a valid enum value.");

        RuleFor(x => x.CreateDealDto.FoodListingType)
            .IsInEnum()
            .WithMessage("Food Listing Type must be a valid enum value.");
    }

    private bool BeAValidDate(DateTime arg)
    {
        return !arg.Equals(default(DateTime));
    }
}