using Byd.GtfsManagement.Dtos.Agencies;

namespace Byd.GtfsManagement.Services.Agencies;

// islevi: agency.txt kayitlarinin CRUD ve liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface IAgencyAppService
    : IGtfsCrudAppService<CreateAgencyDto, UpdateAgencyDto, AgencyDto, AgencyFilter>
{
}
