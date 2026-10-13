using System.Collections.Generic;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.CarsAlloweds;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Lookups.CarsAlloweds;

// islevi: cars_allowed kodlarindan veritabaninda eksik olanlari belirler.
// sistemdeki gorevi: Seed isleminin mevcut lookup satirlarini degistirmeden tamamlanmasini saglar.
public class CarsAllowedManager(IRepository<CarsAllowed, int> repository)
    : BaseLookupManager<CarsAllowed>(repository)
{
    // GTFS cars_allowed alaninin sabit kodlarini Domain.Shared uzerinden alir.
    protected override IReadOnlyList<(int Id, string Name, int Code)> GetDefinitions()
        => CarsAllowedConsts.Definitions;

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    protected override CarsAllowed CreateEntity(int id, string name, int code) => new(id, name, code);
}
