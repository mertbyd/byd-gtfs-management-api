using Byd.GtfsManagement.Data.Lookups;
using Byd.GtfsManagement.Entities.Lookup.CemvSupports;
using Byd.GtfsManagement.Managers.Lookups.CemvSupports;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Data.Lookups.CemvSupports;

// islevi: GTFS cEMV destek kodlarini idempotent olarak ekler.
// sistemdeki gorevi: Agency ve Route lookup baglarinin standart kodlara dayanmasini saglar.
public class CemvSupportDataSeeder(
    CemvSupportManager manager,
    IRepository<CemvSupport, int> repository) : BaseLookupDataSeeder<CemvSupport, CemvSupportManager>(manager, repository);