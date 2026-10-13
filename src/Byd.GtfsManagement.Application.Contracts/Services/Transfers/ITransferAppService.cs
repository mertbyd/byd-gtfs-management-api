using Byd.GtfsManagement.Dtos.Transfers;

namespace Byd.GtfsManagement.Services.Transfers;

// islevi: Transfer CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS aktarma CRUD API sozlesmesini bildirir.</summary>
public interface ITransferAppService
    : IGtfsCrudAppService<CreateTransferDto, UpdateTransferDto, TransferDto, TransferFilter>
{
}
