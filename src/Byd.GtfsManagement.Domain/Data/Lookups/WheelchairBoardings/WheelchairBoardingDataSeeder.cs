using Byd.GtfsManagement.Data.Lookups;
using Byd.GtfsManagement.Entities.Lookup.WheelchairBoardings;
using Byd.GtfsManagement.Managers.Lookups.WheelchairBoardings;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Data.Lookups.WheelchairBoardings;

// islevi: GTFS wheelchair_boarding kodlarini idempotent olarak ekler.
// sistemdeki gorevi: Durak ve sefer erisim baglarinin standart lookup kodlarina dayanmasini saglar.
public class WheelchairBoardingDataSeeder(
    WheelchairBoardingManager manager,
    IRepository<WheelchairBoarding, int> repository)
    : BaseLookupDataSeeder<WheelchairBoarding, WheelchairBoardingManager>(manager, repository);
