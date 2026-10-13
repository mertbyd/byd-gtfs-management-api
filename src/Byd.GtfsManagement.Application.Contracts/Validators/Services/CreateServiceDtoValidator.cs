using FluentValidation;
using Byd.GtfsManagement.Dtos.Services;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Services;

// mirastan gelen: GtfsServiceId zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateServiceDtoValidator : ServiceInputDtoValidatorBase<CreateServiceDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateServiceDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(ServiceExceptionCodes.FeedRequired);
    }
}
