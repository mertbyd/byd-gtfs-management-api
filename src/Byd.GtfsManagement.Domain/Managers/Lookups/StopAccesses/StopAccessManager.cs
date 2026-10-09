using System.Collections.Generic;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.StopAccesses;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Lookups.StopAccesses;

// islevi: stop_access kodlarindan veritabaninda eksik olanlari belirler.
// sistemdeki gorevi: Seed isleminin mevcut lookup satirlarini degistirmeden tamamlanmasini saglar.
public class StopAccessManager(IRepository<StopAccess, int> repository)
    : BaseLookupManager<StopAccess>(repository)
{
    // GTFS stop_access alaninin sabit kodlarini Domain.Shared uzerinden alir.
    protected override IReadOnlyList<(int Id, string Name, int Code)> GetDefinitions()
        => StopAccessConsts.Definitions;

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    protected override StopAccess CreateEntity(int id, string name, int code) => new(id, name, code);
}
