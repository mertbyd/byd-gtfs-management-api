using System.Collections.Generic;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.LocationTypes;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Lookups.LocationTypes;

// islevi: GTFS location_type kodlarindan veritabaninda eksik olanlari belirler.
// sistemdeki gorevi: Standart konum turlerini mevcut lookup satirlarini degistirmeden kurar.
public class LocationTypeManager(IRepository<LocationType, int> repository)
    : BaseLookupManager<LocationType>(repository)
{
    // GTFS location_type alaninin sabit kodlarini Domain.Shared uzerinden alir.
    protected override IReadOnlyList<(int Id, string Name, int Code)> GetDefinitions()
        => LocationTypeConsts.Definitions;

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    protected override LocationType CreateEntity(int id, string name, int code) => new(id, name, code);
}
