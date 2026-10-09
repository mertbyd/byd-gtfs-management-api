using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Routes;

namespace Byd.GtfsManagement.Interfaces.Routes;

// islevi: Route icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
public interface IRouteRepository : IExtendedRepository<Route>
{
    // Duraklara ugrayan guzergahlari ayrintilariyla, siralama alanina gore getirir.
    Task<List<Route>> GetAtStopsAsync(IReadOnlyCollection<Guid> stopIds, CancellationToken cancellationToken = default);
}
