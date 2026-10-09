using Byd.GtfsManagement.Data.Lookups;
using Byd.GtfsManagement.Entities.Lookup.CarsAlloweds;
using Byd.GtfsManagement.Managers.Lookups.CarsAlloweds;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Data.Lookups.CarsAlloweds;

// islevi: cars_allowed kodlarini idempotent olarak ekler.
// sistemdeki gorevi: Sefer baglarinin standart lookup kodlarina dayanmasini saglar.
public class CarsAllowedDataSeeder(
    CarsAllowedManager manager,
    IRepository<CarsAllowed, int> repository)
    : BaseLookupDataSeeder<CarsAllowed, CarsAllowedManager>(manager, repository);
