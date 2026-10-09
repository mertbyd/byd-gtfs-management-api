using Byd.GtfsManagement.Data.Lookups;
using Byd.GtfsManagement.Entities.Lookup.RouteTypes;
using Byd.GtfsManagement.Managers.Lookups.RouteTypes;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Data.Lookups.RouteTypes;

// islevi: route_type kodlarini idempotent olarak ekler.
// sistemdeki gorevi: Guzergah baglarinin standart lookup kodlarina dayanmasini saglar.
public class RouteTypeDataSeeder(
    RouteTypeManager manager,
    IRepository<RouteType, int> repository)
    : BaseLookupDataSeeder<RouteType, RouteTypeManager>(manager, repository);
