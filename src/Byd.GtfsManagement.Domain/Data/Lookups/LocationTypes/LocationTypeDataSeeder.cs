using Byd.GtfsManagement.Data.Lookups;
using Byd.GtfsManagement.Entities.Lookup.LocationTypes;
using Byd.GtfsManagement.Managers.Lookups.LocationTypes;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Data.Lookups.LocationTypes;

// islevi: GTFS location_type kodlarini idempotent olarak ekler.
// sistemdeki gorevi: Durak konum turu baglarinin standart lookup kodlarina dayanmasini saglar.
public class LocationTypeDataSeeder(
    LocationTypeManager manager,
    IRepository<LocationType, int> repository) : BaseLookupDataSeeder<LocationType, LocationTypeManager>(manager, repository);