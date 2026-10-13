using FluentValidation;
using Byd.GtfsManagement.Dtos.Timeframes;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Timeframes;

// mirastan gelen: timeframes.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateTimeframeDtoValidator : TimeframeInputDtoValidatorBase<CreateTimeframeDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateTimeframeDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(TimeframeExceptionCodes.FeedRequired);
    }
}
