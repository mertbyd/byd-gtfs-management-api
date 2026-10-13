using Byd.GtfsManagement.Dtos.Transfers;
using Byd.GtfsManagement.Entities.Transfers;
using Byd.GtfsManagement.Interfaces.Transfers;
using Byd.GtfsManagement.Managers.Transfers;
using Byd.GtfsManagement.Mappers.Transfers;
using Byd.GtfsManagement.Models.Transfers;

namespace Byd.GtfsManagement.Services.Transfers;

// islevi: Transfer CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS aktarma CRUD servisini ortak tabana baglar.</summary>
public class TransferAppService
    : GtfsCrudAppServiceBase<
        Transfer, CreateTransferDto, UpdateTransferDto, TransferDto, TransferFilter,
        TransferCreateModel, TransferUpdateModel, TransferManager, ITransferRepository, TransferMapper>,
        ITransferAppService
{
}
