using FluentValidation;
using Byd.GtfsManagement.Dtos.RiderCategories;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.RiderCategories;

// mirastan gelen: rider_categories.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateRiderCategoryDtoValidator : RiderCategoryInputDtoValidatorBase<CreateRiderCategoryDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateRiderCategoryDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(RiderCategoryExceptionCodes.FeedRequired);
    }
}
