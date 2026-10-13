using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.RiderCategories;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.RiderCategories;

// islevi: RiderCategory ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS yolcu kategorisi ortak girdi kurallarini tanimlar.</summary>
public abstract class RiderCategoryInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : RiderCategoryInputDto
{
    protected RiderCategoryInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsRiderCategoryId)
            .NotEmpty().WithErrorCode(RiderCategoryExceptionCodes.GtfsRiderCategoryIdRequired)
            .MaximumLength(RiderCategoryConsts.MaxGtfsRiderCategoryIdLength).WithErrorCode(RiderCategoryExceptionCodes.GtfsRiderCategoryIdTooLong);
        RuleFor(input => input.RiderCategoryName)
            .NotEmpty().WithErrorCode(RiderCategoryExceptionCodes.RiderCategoryNameRequired)
            .MaximumLength(RiderCategoryConsts.MaxRiderCategoryNameLength).WithErrorCode(RiderCategoryExceptionCodes.RiderCategoryNameTooLong);
        RuleFor(input => input.EligibilityUrl)
            .MaximumLength(RiderCategoryConsts.MaxEligibilityUrlLength)
            .When(input => !string.IsNullOrEmpty(input.EligibilityUrl))
            .WithErrorCode(RiderCategoryExceptionCodes.EligibilityUrlTooLong);
    }
}
