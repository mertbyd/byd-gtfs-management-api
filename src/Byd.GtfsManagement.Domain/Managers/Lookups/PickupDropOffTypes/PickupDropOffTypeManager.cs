using System.Collections.Generic;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.PickupDropOffTypes;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Lookups.PickupDropOffTypes;

// islevi: pickup_type ve drop_off_type kodlarindan veritabaninda eksik olanlari belirler.
// sistemdeki gorevi: Seed isleminin mevcut lookup satirlarini degistirmeden tamamlanmasini saglar.
/// <summary>Eksik binis-inis lookup kodlarini belirler.</summary>
public class PickupDropOffTypeManager(IRepository<PickupDropOffType, int> repository)
    : BaseLookupManager<PickupDropOffType>(repository)
{
    // GTFS pickup_type ve drop_off_type alaninin sabit kodlarini Domain.Shared uzerinden alir.
    protected override IReadOnlyList<(int Id, string Name, int Code)> GetDefinitions()
        => PickupDropOffTypeConsts.Definitions;

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    protected override PickupDropOffType CreateEntity(int id, string name, int code) => new(id, name, code);
}
