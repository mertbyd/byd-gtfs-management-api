using FluentValidation;
using Byd.GtfsManagement.Dtos.LocationGroupStops;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.LocationGroupStops;

// mirastan gelen: location_group_stops.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateLocationGroupStopDtoValidator : LocationGroupStopInputDtoValidatorBase<CreateLocationGroupStopDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateLocationGroupStopDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(LocationGroupStopExceptionCodes.FeedRequired);
    }
}
