using FluentValidation;
using Byd.GtfsManagement.Dtos.Agencies;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Agencies;

// mirastan gelen: agency.txt alanlarinin zorunluluk, uzunluk ve format kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateAgencyDtoValidator : AgencyInputDtoValidatorBase<CreateAgencyDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateAgencyDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(AgencyExceptionCodes.FeedRequired);
    }
}
