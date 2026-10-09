using FluentValidation;
using Byd.GtfsManagement.Dtos.FareMedias;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareMedias;

// mirastan gelen: fare_media.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateFareMediaDtoValidator : FareMediaInputDtoValidatorBase<CreateFareMediaDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateFareMediaDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(FareMediaExceptionCodes.FeedRequired);
    }
}
