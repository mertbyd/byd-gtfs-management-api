using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Pathways;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Pathways;

// islevi: pathways.txt ortak yol, durak, mod ve olcu kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS yuruyus yolu ortak girdi kurallarini tanimlar.</summary>
public abstract class PathwayInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : PathwayInputDto
{
    // Yol kimligi, durak baglari, mod, cift yon ve opsiyonel deger kurallarini kaydeder.
    protected PathwayInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsPathwayId)
            .NotEmpty().WithErrorCode(PathwayExceptionCodes.PathwayRequired)
            .MaximumLength(PathwayConsts.MaxGtfsPathwayIdLength).WithErrorCode(PathwayExceptionCodes.PathwayTooLong);

        RuleFor(input => input.FromStopId)
            .NotEmpty().WithErrorCode(PathwayExceptionCodes.FromStopRequired);

        RuleFor(input => input.ToStopId)
            .NotEmpty().WithErrorCode(PathwayExceptionCodes.ToStopRequired);

        RuleFor(input => input)
            .Must(input => input.FromStopId != input.ToStopId)
            .When(input => input.FromStopId != System.Guid.Empty && input.ToStopId != System.Guid.Empty)
            .WithErrorCode(PathwayExceptionCodes.SameStopForbidden);

        RuleFor(input => input.PathwayMode)
            .InclusiveBetween(PathwayConsts.Walkway, PathwayConsts.ExitGate)
            .WithErrorCode(PathwayExceptionCodes.ModeInvalid);

        RuleFor(input => input.IsBidirectional)
            .Must(val => val is PathwayConsts.Unidirectional or PathwayConsts.Bidirectional)
            .WithErrorCode(PathwayExceptionCodes.BidirectionalInvalid);

        RuleFor(input => input.Length)
            .Must(len => !len.HasValue || (len.Value >= 0 && !double.IsNaN(len.Value) && !double.IsInfinity(len.Value)))
            .WithErrorCode(PathwayExceptionCodes.LengthInvalid);

        RuleFor(input => input.TraversalTime)
            .GreaterThanOrEqualTo(0)
            .When(input => input.TraversalTime.HasValue)
            .WithErrorCode(PathwayExceptionCodes.TraversalTimeInvalid);

        RuleFor(input => input.MinWidth)
            .Must(width => !width.HasValue || (width.Value >= 0 && !double.IsNaN(width.Value) && !double.IsInfinity(width.Value)))
            .WithErrorCode(PathwayExceptionCodes.MinWidthInvalid);

        RuleFor(input => input.SignpostedAs)
            .MaximumLength(PathwayConsts.MaxSignpostedAsLength)
            .When(input => !string.IsNullOrEmpty(input.SignpostedAs))
            .WithErrorCode(PathwayExceptionCodes.SignpostedTooLong);

        RuleFor(input => input.ReversedSignpostedAs)
            .MaximumLength(PathwayConsts.MaxSignpostedAsLength)
            .When(input => !string.IsNullOrEmpty(input.ReversedSignpostedAs))
            .WithErrorCode(PathwayExceptionCodes.SignpostedTooLong);
    }
}
