using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.Models.Routes;

namespace Byd.GtfsManagement.Interfaces.Trips;

// islevi: Trip icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
public interface ITripRepository : IExtendedRepository<Trip>
{
    // Guzergahin seferlerini yon ve tabelaya gore gruplayip her kalibin sayisini ve ayrintili ornek seferini getirir.
    Task<List<RoutePatternModel>> GetPatternsAsync(Guid routeId, CancellationToken cancellationToken = default);
}
