using Byd.GtfsManagement.Dtos.Areas;

namespace Byd.GtfsManagement.Services.Areas;

// islevi: Area CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS bolge CRUD API sozlesmesini bildirir.</summary>
public interface IAreaAppService
    : IGtfsCrudAppService<CreateAreaDto, UpdateAreaDto, AreaDto, AreaFilter>
{
}
