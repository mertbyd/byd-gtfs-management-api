using System.Collections.Generic;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.RouteTypes;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Lookups.RouteTypes;

// islevi: route_type kodlarindan veritabaninda eksik olanlari belirler.
// sistemdeki gorevi: Seed isleminin mevcut lookup satirlarini degistirmeden tamamlanmasini saglar.
public class RouteTypeManager(IRepository<RouteType, int> repository)
    : BaseLookupManager<RouteType>(repository)
{
    // GTFS route_type alaninin sabit kodlarini Domain.Shared uzerinden alir.
    protected override IReadOnlyList<(int Id, string Name, int Code)> GetDefinitions()
        => RouteTypeConsts.Definitions;

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    protected override RouteType CreateEntity(int id, string name, int code) => new(id, name, code);
}
