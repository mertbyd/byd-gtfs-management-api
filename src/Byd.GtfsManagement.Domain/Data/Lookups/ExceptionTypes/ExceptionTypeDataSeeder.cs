using Byd.GtfsManagement.Data.Lookups;
using Byd.GtfsManagement.Entities.Lookup.ExceptionTypes;
using Byd.GtfsManagement.Managers.Lookups.ExceptionTypes;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Data.Lookups.ExceptionTypes;

// islevi: GTFS exception_type kodlarini idempotent olarak ekler.
// sistemdeki gorevi: Takvim istisnalarinin standart lookup kodlarina dayanmasini saglar.
public class ExceptionTypeDataSeeder(
    ExceptionTypeManager manager,
    IRepository<ExceptionType, int> repository)
    : BaseLookupDataSeeder<ExceptionType, ExceptionTypeManager>(manager, repository);
