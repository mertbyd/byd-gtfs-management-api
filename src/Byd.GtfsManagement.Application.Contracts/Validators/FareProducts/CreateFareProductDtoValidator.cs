using FluentValidation;
using Byd.GtfsManagement.Dtos.FareProducts;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareProducts;

// mirastan gelen: fare_products.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateFareProductDtoValidator : FareProductInputDtoValidatorBase<CreateFareProductDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateFareProductDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(FareProductExceptionCodes.FeedRequired);
    }
}
