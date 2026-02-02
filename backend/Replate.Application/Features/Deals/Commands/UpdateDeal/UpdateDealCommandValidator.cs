﻿using FluentValidation;

namespace Replate.Application.Features.Deals.Commands.UpdateDeal;

public class UpdateDealCommandValidator: AbstractValidator<UpdateDealCommand>
{
    public UpdateDealCommandValidator()
    {
        RuleFor(x => x.DealId)
            .NotEmpty()
            .WithMessage("DealId is required.");
        
        RuleFor(x => x.UpdateDealDto)
            .NotNull().WithMessage("UpdateDealDto cannot be null.");

        // Title: Only validate if provided
        When(x => x.UpdateDealDto.Title != null, () => {
            RuleFor(x => x.UpdateDealDto.Title)
                .MaximumLength(100)
                .WithMessage("Title must not exceed 100 characters.");
        });
        // Description: Only validate if provided
        When(x => x.UpdateDealDto.Description != null, () => {
            RuleFor(x => x.UpdateDealDto.Description)
                .MaximumLength(1000)
                .WithMessage("Description cannot exceed 1000 characters.");
        });
        // OriginalPrice: Only validate if provided
        When(x => x.UpdateDealDto.OriginalPrice.HasValue, () => {
            RuleFor(x => x.UpdateDealDto.OriginalPrice!.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("OriginalPrice must be greater than or equal to 0.");
        });
        // DiscountedPrice: Only validate if provided
        When(x => x.UpdateDealDto.DiscountedPrice.HasValue, () => {
            RuleFor(x => x.UpdateDealDto.DiscountedPrice!.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("DiscountedPrice must be greater than or equal to 0.");
        });
        // DiscountedPrice < OriginalPrice: Only if both provided
        When(x => x.UpdateDealDto is { OriginalPrice: not null, DiscountedPrice: not null }, () => {
            RuleFor(x => x.UpdateDealDto)
                .Must(dto => dto.DiscountedPrice <= dto.OriginalPrice)
                .WithMessage("DiscountedPrice must be less than or equal to OriginalPrice.");
        });
        // FoodListingType: Only validate if provided
        When(x => x.UpdateDealDto.FoodListingType.HasValue, () => {
            RuleFor(x => x.UpdateDealDto.FoodListingType!.Value)
                .IsInEnum()
                .WithMessage("FoodListingType is invalid.");
        });
        // Category: Only validate if provided
        When(x => x.UpdateDealDto.Category.HasValue, () => {
            RuleFor(x => x.UpdateDealDto.Category!.Value)
                .IsInEnum()
                .WithMessage("Category is invalid.");
        });
        // AvailableFrom: Only validate if provided
        When(x => x.UpdateDealDto.AvailableFrom.HasValue, () => {
            RuleFor(x => x.UpdateDealDto.AvailableFrom!.Value)
                .Must(BeAValidDate)
                .WithMessage("AvailableFrom must be a valid date.");
        });
        // AvailableUntil: Only validate if provided
        When(x => x.UpdateDealDto.AvailableUntil.HasValue, () => {
            RuleFor(x => x.UpdateDealDto.AvailableUntil!.Value)
                .Must(BeAValidDate)
                .WithMessage("AvailableUntil must be a valid date.");
        });
        // VendorProfileId: Only validate if provided
        When(x => x.UpdateDealDto.VendorProfileId.HasValue, () => {
            RuleFor(x => x.UpdateDealDto.VendorProfileId!.Value)
                .GreaterThan(0)
                .WithMessage("VendorProfileId must be greater than 0.");
        });
    }

    private bool BeAValidDate(DateTime arg)
    {
        return !arg.Equals(default(DateTime));
    }
}
