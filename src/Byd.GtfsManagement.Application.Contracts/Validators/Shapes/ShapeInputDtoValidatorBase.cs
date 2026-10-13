using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Shapes;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Shapes;

// islevi: shapes.txt ortak sekil ve zorunlu alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS sekil ortak girdi kurallarini tanimlar.</summary>
public abstract class ShapeInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : ShapeInputDto
{
    // Sekil kimligi, koordinat araliklari, sira ve mesafe kurallarini kaydeder.
    protected ShapeInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsShapeId)
            .NotEmpty().WithErrorCode(ShapeExceptionCodes.ShapeRequired)
            .MaximumLength(ShapeConsts.MaxGtfsShapeIdLength).WithErrorCode(ShapeExceptionCodes.ShapeTooLong);

        RuleFor(input => input.Latitude)
            .InclusiveBetween(ShapeConsts.MinLatitude, ShapeConsts.MaxLatitude)
            .WithErrorCode(ShapeExceptionCodes.LatitudeInvalid);

        RuleFor(input => input.Longitude)
            .InclusiveBetween(ShapeConsts.MinLongitude, ShapeConsts.MaxLongitude)
            .WithErrorCode(ShapeExceptionCodes.LongitudeInvalid);

        RuleFor(input => input.Sequence)
            .GreaterThanOrEqualTo(0)
            .WithErrorCode(ShapeExceptionCodes.SequenceInvalid);

        RuleFor(input => input.DistTraveled)
            .GreaterThanOrEqualTo(0)
            .When(input => input.DistTraveled.HasValue)
            .WithErrorCode(ShapeExceptionCodes.DistTraveledInvalid);
    }
}
