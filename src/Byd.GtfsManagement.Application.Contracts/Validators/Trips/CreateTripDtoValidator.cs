using FluentValidation;
using Byd.GtfsManagement.Dtos.Trips;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Trips;

// mirastan gelen: trips.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateTripDtoValidator : TripInputDtoValidatorBase<CreateTripDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateTripDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(TripExceptionCodes.FeedRequired);
    }
}
