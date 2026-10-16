using Byd.GtfsManagement.Dtos.RiderCategories;

namespace Byd.GtfsManagement.Validators.RiderCategories;

// mirastan gelen: rider_categories.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateRiderCategoryDtoValidator : RiderCategoryInputDtoValidatorBase<CreateRiderCategoryDto>
{
}
