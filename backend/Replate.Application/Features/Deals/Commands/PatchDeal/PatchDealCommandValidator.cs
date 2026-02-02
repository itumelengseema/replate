using FluentValidation;
using Replate.Application.Features.Deals.DTOs;

namespace Replate.Application.Features.Deals.Commands.PatchDeal;

public class PatchDealCommandValidator : AbstractValidator<PatchDealCommand>
{
    public PatchDealCommandValidator()
    {
        RuleFor(x => x.DealId)
            .NotEmpty()
            .WithMessage("DealId is required.");
        
        RuleFor(x => x.PatchDealDto)
            .NotNull().WithMessage("PatchDealDto cannot be null.");

        // Title: Only validate if provided
        When(x => x.PatchDealDto.Title != null, () => {
            RuleFor(x => x.PatchDealDto.Title)
                .MaximumLength(100)
                .WithMessage("Title must not exceed 100 characters.");
        });
        // Description: Only validate if provided
        When(x => x.PatchDealDto.Description != null, () => {
            RuleFor(x => x.PatchDealDto.Description)
                .MaximumLength(1000)
                .WithMessage("Description cannot exceed 1000 characters.");
        });
        // OriginalPrice: Only validate if provided
        When(x => x.PatchDealDto.OriginalPrice.HasValue, () => {
            RuleFor(x => x.PatchDealDto.OriginalPrice!.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("OriginalPrice must be greater than or equal to 0.");
        });
        // DiscountedPrice: Only validate if provided
        When(x => x.PatchDealDto.DiscountedPrice.HasValue, () => {
            RuleFor(x => x.PatchDealDto.DiscountedPrice!.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("DiscountedPrice must be greater than or equal to 0.");
        });
        // DiscountedPrice < OriginalPrice: Only if both provided
        When(x => x.PatchDealDto is { OriginalPrice: not null, DiscountedPrice: not null }, () => {
            RuleFor(x => x.PatchDealDto)
                .Must(dto => dto.DiscountedPrice <= dto.OriginalPrice)
                .WithMessage("DiscountedPrice must be less than or equal to OriginalPrice.");
        });
        // DealType: Only validate if provided
        When(x => x.PatchDealDto.DealType.HasValue, () => {
            RuleFor(x => x.PatchDealDto.DealType!.Value)
                .IsInEnum()
                .WithMessage("DealType is invalid.");
        });
        // Category: Only validate if provided
        When(x => x.PatchDealDto.Category.HasValue, () => {
            RuleFor(x => x.PatchDealDto.Category!.Value)
                .IsInEnum()
                .WithMessage("Category is invalid.");
        });
        // AvailableFrom: Only validate if provided
        When(x => x.PatchDealDto.AvailableFrom.HasValue, () => {
            RuleFor(x => x.PatchDealDto.AvailableFrom!.Value)
                .Must(BeAValidDate)
                .WithMessage("AvailableFrom must be a valid date.");
        });
        // AvailableUntil: Only validate if provided
        When(x => x.PatchDealDto.AvailableUntil.HasValue, () => {
            RuleFor(x => x.PatchDealDto.AvailableUntil!.Value)
                .Must(BeAValidDate)
                .WithMessage("AvailableUntil must be a valid date.");
        });
        // VendorProfileId: Only validate if provided
        When(x => x.PatchDealDto.VendorProfileId.HasValue, () => {
            RuleFor(x => x.PatchDealDto.VendorProfileId!.Value)
                .GreaterThan(0)
                .WithMessage("VendorProfileId must be greater than 0.");
        });
    }

    private bool BeAValidDate(DateTime arg)
    {
        return !arg.Equals(default(DateTime));
    }
}