using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Transfers;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Transfers;

// islevi: transfers.txt public alan ve kosullu zorunluluk kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerini ayni GTFS sekil dogrulamasina baglar.
/// <summary>GTFS aktarma ortak girdi kurallarini tanimlar.</summary>
public abstract class TransferInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : TransferInputDto
{
    // Aktarma turu, durak/sefer zorunlulugu ve sure sinirini kaydeder.
    protected TransferInputDtoValidatorBase()
    {
        RuleFor(input => input.TransferType)
            .Must(value => value is null or >= TransferConsts.RecommendedCode and <= TransferConsts.InSeatForbiddenCode)
            .WithErrorCode(TransferExceptionCodes.TypeInvalid);
        RuleFor(input => input.MinTransferTime)
            .GreaterThanOrEqualTo(0).When(input => input.MinTransferTime is not null)
            .WithErrorCode(TransferExceptionCodes.MinimumTimeInvalid);
        RuleFor(input => input.MinTransferTime)
            .NotNull().When(input => input.TransferType == TransferConsts.MinimumTimeCode)
            .WithErrorCode(TransferExceptionCodes.MinimumTimeRequired);
        RuleFor(input => input.FromStopId)
            .NotEmpty().When(input => !IsLinked(input.TransferType))
            .WithErrorCode(TransferExceptionCodes.FromStopRequired);
        RuleFor(input => input.ToStopId)
            .NotEmpty().When(input => !IsLinked(input.TransferType))
            .WithErrorCode(TransferExceptionCodes.ToStopRequired);
        RuleFor(input => input.FromTripId)
            .NotEmpty().When(input => IsLinked(input.TransferType))
            .WithErrorCode(TransferExceptionCodes.FromTripRequired);
        RuleFor(input => input.ToTripId)
            .NotEmpty().When(input => IsLinked(input.TransferType))
            .WithErrorCode(TransferExceptionCodes.ToTripRequired);
    }

    // Bagli sefer davranisini GTFS 4 ve 5 kodlarindan tanir.
    private static bool IsLinked(int? transferType)
        => transferType is TransferConsts.InSeatAllowedCode or TransferConsts.InSeatForbiddenCode;
}
