using Byd.GtfsManagement.Dtos.Services;

namespace Byd.GtfsManagement.Services.Services;

// islevi: Service kayitlarinin CRUD ve liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface IServiceAppService
    : IGtfsCrudAppService<CreateServiceDto, UpdateServiceDto, ServiceDto, ServiceFilter>
{
}
