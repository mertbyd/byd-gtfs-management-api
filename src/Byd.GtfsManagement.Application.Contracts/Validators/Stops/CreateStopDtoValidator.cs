using FluentValidation;
using Byd.GtfsManagement.Dtos.Stops;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Stops;

// mirastan gelen: stops.txt alanlarinin zorunluluk, uzunluk ve koordinat araligi kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateStopDtoValidator : StopInputDtoValidatorBase<CreateStopDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateStopDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(StopExceptionCodes.FeedRequired);
    }
}
