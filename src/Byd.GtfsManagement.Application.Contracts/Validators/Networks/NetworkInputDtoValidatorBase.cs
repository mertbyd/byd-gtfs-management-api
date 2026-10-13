using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Networks;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Networks;

// islevi: Network ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS ulasim agi ortak girdi kurallarini tanimlar.</summary>
public abstract class NetworkInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : NetworkInputDto
{
    protected NetworkInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsNetworkId)
            .NotEmpty().WithErrorCode(NetworkExceptionCodes.GtfsNetworkIdRequired)
            .MaximumLength(NetworkConsts.MaxGtfsNetworkIdLength).WithErrorCode(NetworkExceptionCodes.GtfsNetworkIdTooLong);
        RuleFor(input => input.NetworkName)
            .MaximumLength(NetworkConsts.MaxNetworkNameLength)
            .When(input => !string.IsNullOrEmpty(input.NetworkName))
            .WithErrorCode(NetworkExceptionCodes.NetworkNameTooLong);
    }
}
