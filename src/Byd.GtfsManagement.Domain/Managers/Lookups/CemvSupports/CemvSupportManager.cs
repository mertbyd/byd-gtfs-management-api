using System.Collections.Generic;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.CemvSupports;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Lookups.CemvSupports;

// islevi: GTFS cEMV kodlarindan veritabaninda eksik olanlari belirler.
// sistemdeki gorevi: Seed isleminin mevcut lookup satirlarini degistirmeden tamamlanmasini saglar.
public class CemvSupportManager(IRepository<CemvSupport, int> repository)
    : BaseLookupManager<CemvSupport>(repository)
{
    // GTFS cemv_support alaninin sabit kodlarini Domain.Shared uzerinden alir.
    protected override IReadOnlyList<(int Id, string Name, int Code)> GetDefinitions()
        => CemvSupportConsts.Definitions;

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    protected override CemvSupport CreateEntity(int id, string name, int code) => new(id, name, code);
}
