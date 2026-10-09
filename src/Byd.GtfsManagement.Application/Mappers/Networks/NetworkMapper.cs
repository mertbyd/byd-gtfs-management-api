using Byd.GtfsManagement.Dtos.Networks;
using Byd.GtfsManagement.Entities.Networks;
using Byd.GtfsManagement.Models.Networks;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Networks;

// islevi: Network DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS ulasim agi DTO ve modellerini esler.</summary>
[Mapper]
public partial class NetworkMapper
    : IGtfsCrudMapper<Network, CreateNetworkDto, UpdateNetworkDto, NetworkDto,
        NetworkCreateModel, NetworkUpdateModel>
{
    public partial NetworkCreateModel Map(CreateNetworkDto input);
    public partial NetworkUpdateModel Map(UpdateNetworkDto input);
    public partial NetworkDto Map(Network entity);
}
