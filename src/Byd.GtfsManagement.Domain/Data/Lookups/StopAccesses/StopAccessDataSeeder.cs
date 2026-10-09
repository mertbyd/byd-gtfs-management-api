using Byd.GtfsManagement.Data.Lookups;
using Byd.GtfsManagement.Entities.Lookup.StopAccesses;
using Byd.GtfsManagement.Managers.Lookups.StopAccesses;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Data.Lookups.StopAccesses;

// islevi: stop_access kodlarini idempotent olarak ekler.
// sistemdeki gorevi: Durak baglarinin standart lookup kodlarina dayanmasini saglar.
public class StopAccessDataSeeder(
    StopAccessManager manager,
    IRepository<StopAccess, int> repository)
    : BaseLookupDataSeeder<StopAccess, StopAccessManager>(manager, repository);
