using Byd.GtfsManagement.Dtos.FareProducts;

namespace Byd.GtfsManagement.Validators.FareProducts;

// mirastan gelen: fare_products.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateFareProductDtoValidator : FareProductInputDtoValidatorBase<CreateFareProductDto>
{
}
