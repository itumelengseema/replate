using FluentValidation;

namespace Replate.Application.Features.Deals.Commands.CreateDeal;

public class CreateDealCommandValidator : AbstractValidator<CreateDealCommand>
{
    public CreateDealCommandValidator()
    {
        RuleFor(x => x.Deal)
            .NotNull()
            .WithMessage("Deal information must be provided.");

        When(x => x.Deal != null, () =>
        {
            RuleFor(x => x.Deal.Title)
                .NotEmpty()
                .WithMessage("Deal Title is required.")
                .MaximumLength(200)
                .WithMessage("Deal Title cannot exceed 200 characters.");

            RuleFor(x => x.Deal.Description)
                .NotEmpty()
                .WithMessage("Deal Description is required.")
                .MaximumLength(1000)
                .WithMessage("Deal Description cannot exceed 1000 characters.");

            RuleFor(x => x.Deal.OriginalPrice)
                .GreaterThan(0)
                .WithMessage("Deal Price must be a positive value.");

            RuleFor(x => x.Deal.DiscountedPrice)
                .GreaterThan(0)
                .WithMessage("Discounted Price must be a positive value.")
                .LessThan(x => x.Deal.OriginalPrice)
                .WithMessage("Discounted Price must be less than Original Price.");

            RuleFor(x => x.Deal.AvailableQuantity)
                .GreaterThan(0)
                .WithMessage("Available Quantity must be a positive integer.");

            RuleFor(x => x.Deal.AvailableFrom)
                .Must(BeAValidDate)
                .WithMessage("Available From must be a valid date.");

            RuleFor(x => x.Deal.AvailableUntil)
                .Must(BeAValidDate)
                .WithMessage("Available Until must be a valid date.");

            RuleFor(x => x.Deal)
                .Must(d => d.AvailableFrom < d.AvailableUntil)
                .WithMessage("Available From must be before Available Until.");

            RuleFor(x => x.Deal.Category)
                .IsInEnum()
                .WithMessage("Category must be a valid enum value.");

            RuleFor(x => x.Deal.DealType)
                .IsInEnum()
                .WithMessage("Deal Type must be a valid enum value.");
        });
    }

    private bool BeAValidDate(DateTime arg)
    {
        return !arg.Equals(default(DateTime));
    }
}