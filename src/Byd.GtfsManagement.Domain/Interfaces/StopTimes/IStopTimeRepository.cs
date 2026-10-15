using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.StopTimes;
using Byd.GtfsManagement.Models.StopTimes;

namespace Byd.GtfsManagement.Interfaces.StopTimes;

// islevi: StopTime icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
/// <summary>StopTime veri erisim sozlesmesini bildirir.</summary>
public interface IStopTimeRepository : IExtendedRepository<StopTime>
{
    // Duraklardan verilen servislerle ve saatten sonra kalkan ziyaretleri kalkis sirasiyla, sefer ve guzergahiyla getirir.
    Task<List<StopTime>> GetDeparturesAsync(
        IReadOnlyCollection<Guid> stopIds, IReadOnlyCollection<Guid> serviceIds, string fromTime, int limit,
        CancellationToken cancellationToken = default);

    // Seferin durak ziyaretlerini durak ve lookup adlariyla sira numarasina gore getirir.
    Task<List<StopTime>> GetTripStopsAsync(Guid tripId, CancellationToken cancellationToken = default);

    // Verilen servislerin, saat araliginda kalkan veya varan durak ziyaretlerini getirir.
    Task<List<StopVisitModel>> GetVisitsAsync(
        Guid? feedId, IReadOnlyCollection<Guid> serviceIds, string fromTime, string toTime,
        CancellationToken cancellationToken = default);
}
