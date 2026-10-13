using Byd.GtfsManagement.Data.Lookups;
using Byd.GtfsManagement.Entities.Lookup.TripDirections;
using Byd.GtfsManagement.Managers.Lookups.TripDirections;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Data.Lookups.TripDirections;

// islevi: direction_id kodlarini idempotent olarak ekler.
// sistemdeki gorevi: Sefer baglarinin standart lookup kodlarina dayanmasini saglar.
public class TripDirectionDataSeeder(
    TripDirectionManager manager,
    IRepository<TripDirection, int> repository)
    : BaseLookupDataSeeder<TripDirection, TripDirectionManager>(manager, repository);
