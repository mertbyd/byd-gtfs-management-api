using Byd.GtfsManagement.Dtos.LocationGroups;

namespace Byd.GtfsManagement.Validators.LocationGroups;

// mirastan gelen: location_groups.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateLocationGroupDtoValidator : LocationGroupInputDtoValidatorBase<CreateLocationGroupDto>
{
}
