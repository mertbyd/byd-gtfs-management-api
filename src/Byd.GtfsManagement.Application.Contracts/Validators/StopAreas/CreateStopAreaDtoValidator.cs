using FluentValidation;
using Byd.GtfsManagement.Dtos.StopAreas;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.StopAreas;

// mirastan gelen: stop_areas.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateStopAreaDtoValidator : StopAreaInputDtoValidatorBase<CreateStopAreaDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateStopAreaDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(StopAreaExceptionCodes.FeedRequired);
    }
}
