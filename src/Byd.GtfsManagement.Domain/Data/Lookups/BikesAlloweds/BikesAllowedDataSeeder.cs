using Byd.GtfsManagement.Data.Lookups;
using Byd.GtfsManagement.Entities.Lookup.BikesAlloweds;
using Byd.GtfsManagement.Managers.Lookups.BikesAlloweds;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Data.Lookups.BikesAlloweds;

// islevi: bikes_allowed kodlarini idempotent olarak ekler.
// sistemdeki gorevi: Sefer baglarinin standart lookup kodlarina dayanmasini saglar.
public class BikesAllowedDataSeeder(
    BikesAllowedManager manager,
    IRepository<BikesAllowed, int> repository)
    : BaseLookupDataSeeder<BikesAllowed, BikesAllowedManager>(manager, repository);
