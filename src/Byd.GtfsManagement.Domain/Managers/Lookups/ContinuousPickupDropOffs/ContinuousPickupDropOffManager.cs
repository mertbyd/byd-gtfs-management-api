using System.Collections.Generic;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.ContinuousPickupDropOffs;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Lookups.ContinuousPickupDropOffs;

// islevi: continuous_pickup ve continuous_drop_off kodlarindan veritabaninda eksik olanlari belirler.
// sistemdeki gorevi: Seed isleminin mevcut lookup satirlarini degistirmeden tamamlanmasini saglar.
public class ContinuousPickupDropOffManager(IRepository<ContinuousPickupDropOff, int> repository)
    : BaseLookupManager<ContinuousPickupDropOff>(repository)
{
    // GTFS continuous_pickup ve continuous_drop_off alaninin sabit kodlarini Domain.Shared uzerinden alir.
    protected override IReadOnlyList<(int Id, string Name, int Code)> GetDefinitions()
        => ContinuousPickupDropOffConsts.Definitions;

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    protected override ContinuousPickupDropOff CreateEntity(int id, string name, int code) => new(id, name, code);
}
