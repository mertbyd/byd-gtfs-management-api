using Byd.GtfsManagement.Dtos.Networks;

namespace Byd.GtfsManagement.Services.Networks;

// islevi: Network CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS ulasim agi CRUD API sozlesmesini bildirir.</summary>
public interface INetworkAppService
    : IGtfsCrudAppService<CreateNetworkDto, UpdateNetworkDto, NetworkDto, NetworkFilter>
{
}
