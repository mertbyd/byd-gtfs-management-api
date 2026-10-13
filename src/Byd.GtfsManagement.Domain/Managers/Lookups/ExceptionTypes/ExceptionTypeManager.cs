using System.Collections.Generic;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.ExceptionTypes;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Lookups.ExceptionTypes;

// islevi: GTFS exception_type kodlarindan veritabaninda eksik olanlari belirler.
// sistemdeki gorevi: Seed isleminin mevcut lookup satirlarini degistirmeden tamamlanmasini saglar.
public class ExceptionTypeManager(IRepository<ExceptionType, int> repository)
    : BaseLookupManager<ExceptionType>(repository)
{
    // GTFS exception_type alaninin sabit kodlarini Domain.Shared uzerinden alir.
    protected override IReadOnlyList<(int Id, string Name, int Code)> GetDefinitions()
        => ExceptionTypeConsts.Definitions;

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    protected override ExceptionType CreateEntity(int id, string name, int code) => new(id, name, code);
}
