using FluentValidation;

namespace Replate.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.CreateReservationDto.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");

        RuleFor(x => x.CreateReservationDto.TotalPrice)
            .GreaterThan(0)
            .WithMessage("TotalPrice must be greater than 0");

        RuleFor(x => x.CreateReservationDto.PickupTime)
            .Must(BeValid)
            .WithMessage("Pickup Time must be in the future");

        RuleFor(x => x.CreateReservationDto.PickupInstructions)
            .MaximumLength(500)
            .WithMessage("Pickup Instructions cannot exceed 500 characters.");

        RuleFor(x => x.CreateReservationDto.Status)
            .IsInEnum()
            .WithMessage("Status must be in enum");

        RuleFor(x => x.CreateReservationDto.UserId)
            .GreaterThan(0)
            .WithMessage("UserId is required");

        RuleFor(x => x.CreateReservationDto.DealId)
            .GreaterThan(0)
            .WithMessage("DealId is required");
    }

    private bool BeValid(DateTime pickupTime)
    {
        return pickupTime > DateTime.UtcNow;
    }
}
