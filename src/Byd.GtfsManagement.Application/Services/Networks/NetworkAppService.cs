using Byd.GtfsManagement.Dtos.Networks;
using Byd.GtfsManagement.Entities.Networks;
using Byd.GtfsManagement.Interfaces.Networks;
using Byd.GtfsManagement.Managers.Networks;
using Byd.GtfsManagement.Mappers.Networks;
using Byd.GtfsManagement.Models.Networks;

namespace Byd.GtfsManagement.Services.Networks;

// islevi: Network CRUD ve GTFS arsiv akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS ulasim agi CRUD servisini ortak tabana baglar.</summary>
public class NetworkAppService
    : GtfsImportExportAppServiceBase<
        Network, CreateNetworkDto, UpdateNetworkDto, NetworkDto, NetworkFilter,
        NetworkCreateModel, NetworkUpdateModel, NetworkManager, INetworkRepository, NetworkMapper, NetworkImportDto>,
        INetworkAppService
{
}
