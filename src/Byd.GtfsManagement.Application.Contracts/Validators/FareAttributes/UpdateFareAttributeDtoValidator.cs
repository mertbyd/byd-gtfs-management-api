using Byd.GtfsManagement.Dtos.FareAttributes;

namespace Byd.GtfsManagement.Validators.FareAttributes;

// mirastan gelen: ortak fare_attributes.txt alan kurallari.
// islevi: UpdateFareAttributeDto kurallarini tanimlar.
// sistemdeki gorevi: Guncelleme istegini ortak girdi dogrulamasindan gecirir.
/// <summary>GTFS ucret ozelligi guncelleme dogrulayicisini tanimlar.</summary>
public sealed class UpdateFareAttributeDtoValidator : FareAttributeInputDtoValidatorBase<UpdateFareAttributeDto>
{
}
