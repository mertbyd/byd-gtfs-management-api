using Byd.GtfsManagement.Dtos.Transfers;

namespace Byd.GtfsManagement.Validators.Transfers;

// mirastan gelen: ortak transfers.txt alan kurallari.
// burada yazilan: yok; kapsam degistirilemez.
// islevi: Aktarma guncelleme istegini dogrular.
// sistemdeki gorevi: Ortak validator kurallarini Update sozlesmesine uygular.
/// <summary>GTFS aktarma guncelleme kurallarini tanimlar.</summary>
public sealed class UpdateTransferDtoValidator : TransferInputDtoValidatorBase<UpdateTransferDto>
{
}
