using FluentValidation;
using Byd.GtfsManagement.Dtos.Networks;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Networks;

// mirastan gelen: networks.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateNetworkDtoValidator : NetworkInputDtoValidatorBase<CreateNetworkDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateNetworkDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(NetworkExceptionCodes.FeedRequired);
    }
}
