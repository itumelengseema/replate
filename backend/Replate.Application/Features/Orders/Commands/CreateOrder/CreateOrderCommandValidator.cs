using FluentValidation;

namespace Replate.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID is required");

        RuleFor(x => x.Order.FoodListingPublicId)
            .NotEmpty().WithMessage("Food listing ID is required");

        RuleFor(x => x.Order.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }
}
