using FluentValidation;
using Byd.GtfsManagement.Dtos.LocationGroups;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.LocationGroups;

// mirastan gelen: location_groups.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateLocationGroupDtoValidator : LocationGroupInputDtoValidatorBase<CreateLocationGroupDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateLocationGroupDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(LocationGroupExceptionCodes.FeedRequired);
    }
}
