using System.Collections.Generic;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.BikesAlloweds;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Lookups.BikesAlloweds;

// islevi: bikes_allowed kodlarindan veritabaninda eksik olanlari belirler.
// sistemdeki gorevi: Seed isleminin mevcut lookup satirlarini degistirmeden tamamlanmasini saglar.
public class BikesAllowedManager(IRepository<BikesAllowed, int> repository)
    : BaseLookupManager<BikesAllowed>(repository)
{
    // GTFS bikes_allowed alaninin sabit kodlarini Domain.Shared uzerinden alir.
    protected override IReadOnlyList<(int Id, string Name, int Code)> GetDefinitions()
        => BikesAllowedConsts.Definitions;

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    protected override BikesAllowed CreateEntity(int id, string name, int code) => new(id, name, code);
}
