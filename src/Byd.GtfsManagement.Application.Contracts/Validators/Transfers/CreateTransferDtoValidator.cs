using Byd.GtfsManagement.Dtos.Transfers;

namespace Byd.GtfsManagement.Validators.Transfers;

// mirastan gelen: ortak transfers.txt alan kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
// islevi: Yeni aktarma istegini dogrular.
// sistemdeki gorevi: Olusturma istegini ortak girdi dogrulamasindan gecirir.
/// <summary>GTFS aktarma olusturma kurallarini tanimlar.</summary>
public sealed class CreateTransferDtoValidator : TransferInputDtoValidatorBase<CreateTransferDto>
{
}
