using System.Collections.Generic;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.WheelchairBoardings;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Lookups.WheelchairBoardings;

// islevi: GTFS wheelchair_boarding kodlarindan veritabaninda eksik olanlari belirler.
// sistemdeki gorevi: Seed isleminin mevcut lookup satirlarini degistirmeden tamamlanmasini saglar.
public class WheelchairBoardingManager(IRepository<WheelchairBoarding, int> repository)
    : BaseLookupManager<WheelchairBoarding>(repository)
{
    // GTFS wheelchair_boarding alaninin sabit kodlarini Domain.Shared uzerinden alir.
    protected override IReadOnlyList<(int Id, string Name, int Code)> GetDefinitions()
        => WheelchairBoardingConsts.Definitions;

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    protected override WheelchairBoarding CreateEntity(int id, string name, int code) => new(id, name, code);
}
