﻿using FluentValidation;

 using Replate.Application.Features.FoodListings.Commands.UpdateFoodListing;

 namespace Replate.Application.Features.FoodListings.Commands.UpdateDeal;

public class UpdateFoodListingDtoCommandValidator: AbstractValidator<UpdateFoodListingCommand>
{
    public UpdateFoodListingDtoCommandValidator()
    {
        RuleFor(x => x.FoodListingId)
            .NotEmpty()
            .WithMessage("DealId is required.");
        
        RuleFor(x => x.UpdateFoodListingDto)
            .NotNull().WithMessage("UpdateDealDto cannot be null.");

        // Title: Only validate if provided
        When(x => x.UpdateFoodListingDto.Title != null, () => {
            RuleFor(x => x.UpdateFoodListingDto.Title)
                .MaximumLength(100)
                .WithMessage("Title must not exceed 100 characters.");
        });
        // Description: Only validate if provided
        When(x => x.UpdateFoodListingDto.Description != null, () => {
            RuleFor(x => x.UpdateFoodListingDto.Description)
                .MaximumLength(1000)
                .WithMessage("Description cannot exceed 1000 characters.");
        });
        // OriginalPrice: Only validate if provided
        When(x => x.UpdateFoodListingDto.OriginalPrice.HasValue, () => {
            RuleFor(x => x.UpdateFoodListingDto.OriginalPrice!.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("OriginalPrice must be greater than or equal to 0.");
        });
        // DiscountedPrice: Only validate if provided
        When(x => x.UpdateFoodListingDto.DiscountedPrice.HasValue, () => {
            RuleFor(x => x.UpdateFoodListingDto.DiscountedPrice!.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("DiscountedPrice must be greater than or equal to 0.");
        });
        // DiscountedPrice < OriginalPrice: Only if both provided
        When(x => x.UpdateFoodListingDto is { OriginalPrice: not null, DiscountedPrice: not null }, () => {
            RuleFor(x => x.UpdateFoodListingDto)
                .Must(dto => dto.DiscountedPrice <= dto.OriginalPrice)
                .WithMessage("DiscountedPrice must be less than or equal to OriginalPrice.");
        });
        // FoodListingType: Only validate if provided
        When(x => x.UpdateFoodListingDto.FoodListingType.HasValue, () => {
            RuleFor(x => x.UpdateFoodListingDto.FoodListingType!.Value)
                .IsInEnum()
                .WithMessage("FoodListingType is invalid.");
        });
        // Category: Only validate if provided
        When(x => x.UpdateFoodListingDto.Category.HasValue, () => {
            RuleFor(x => x.UpdateFoodListingDto.Category!.Value)
                .IsInEnum()
                .WithMessage("Category is invalid.");
        });
        // AvailableFrom: Only validate if provided
        When(x => x.UpdateFoodListingDto.AvailableFrom.HasValue, () => {
            RuleFor(x => x.UpdateFoodListingDto.AvailableFrom!.Value)
                .Must(BeAValidDate)
                .WithMessage("AvailableFrom must be a valid date.");
        });
        // AvailableUntil: Only validate if provided
        When(x => x.UpdateFoodListingDto.AvailableUntil.HasValue, () => {
            RuleFor(x => x.UpdateFoodListingDto.AvailableUntil!.Value)
                .Must(BeAValidDate)
                .WithMessage("AvailableUntil must be a valid date.");
        });
        // VendorProfileId: Only validate if provided
        When(x => x.UpdateFoodListingDto.VendorProfileId.HasValue, () => {
            RuleFor(x => x.UpdateFoodListingDto.VendorProfileId!.Value)
                .GreaterThan(0)
                .WithMessage("VendorProfileId must be greater than 0.");
        });
    }

    private bool BeAValidDate(DateTime arg)
    {
        return !arg.Equals(default(DateTime));
    }
}
