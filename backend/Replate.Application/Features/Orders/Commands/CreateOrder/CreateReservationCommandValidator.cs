using FluentValidation;

namespace Replate.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId is required");

        RuleFor(x => x.FoodListingPublicId)
            .NotEmpty()
            .WithMessage("FoodListingPublicId is required");
    }
}
