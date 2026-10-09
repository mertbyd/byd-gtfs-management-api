using Byd.GtfsManagement.Data.Lookups;
using Byd.GtfsManagement.Entities.Lookup.PickupDropOffTypes;
using Byd.GtfsManagement.Managers.Lookups.PickupDropOffTypes;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Data.Lookups.PickupDropOffTypes;

// islevi: pickup_type ve drop_off_type kodlarini idempotent olarak ekler.
// sistemdeki gorevi: Durak ziyaretlerinin standart binis ve inis kodlarina dayanmasini saglar.
/// <summary>GTFS binis-inis lookup satirlarini ekler.</summary>
public class PickupDropOffTypeDataSeeder(
    PickupDropOffTypeManager manager,
    IRepository<PickupDropOffType, int> repository)
    : BaseLookupDataSeeder<PickupDropOffType, PickupDropOffTypeManager>(manager, repository);
