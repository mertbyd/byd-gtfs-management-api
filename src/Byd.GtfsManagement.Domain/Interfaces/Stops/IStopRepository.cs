using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Models.Stops;

namespace Byd.GtfsManagement.Interfaces.Stops;

// islevi: Stop icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
public interface IStopRepository : IExtendedRepository<Stop>
{
    // Feed'in tum duraklarini yolculuk planlayicisi icin hafif modelle getirir.
    Task<List<StopPlaceModel>> GetPlacesAsync(Guid feedId, CancellationToken cancellationToken = default);
}
