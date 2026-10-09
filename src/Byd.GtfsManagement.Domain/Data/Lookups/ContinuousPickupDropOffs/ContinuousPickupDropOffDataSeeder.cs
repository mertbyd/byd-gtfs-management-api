using Byd.GtfsManagement.Data.Lookups;
using Byd.GtfsManagement.Entities.Lookup.ContinuousPickupDropOffs;
using Byd.GtfsManagement.Managers.Lookups.ContinuousPickupDropOffs;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Data.Lookups.ContinuousPickupDropOffs;

// islevi: continuous_pickup ve continuous_drop_off kodlarini idempotent olarak ekler.
// sistemdeki gorevi: Guzergah baglarinin standart lookup kodlarina dayanmasini saglar.
public class ContinuousPickupDropOffDataSeeder(
    ContinuousPickupDropOffManager manager,
    IRepository<ContinuousPickupDropOff, int> repository)
    : BaseLookupDataSeeder<ContinuousPickupDropOff, ContinuousPickupDropOffManager>(manager, repository);
