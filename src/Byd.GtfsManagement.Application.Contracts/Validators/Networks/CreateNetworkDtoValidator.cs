using Byd.GtfsManagement.Dtos.Networks;

namespace Byd.GtfsManagement.Validators.Networks;

// mirastan gelen: networks.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateNetworkDtoValidator : NetworkInputDtoValidatorBase<CreateNetworkDto>
{
}
