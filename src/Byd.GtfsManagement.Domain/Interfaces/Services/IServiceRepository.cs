using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Services;

namespace Byd.GtfsManagement.Interfaces.Services;

// islevi: Service icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
public interface IServiceRepository : IExtendedRepository<Service>
{
    // Verilen gunde calisan servislerin kimliklerini calendar ve calendar_dates kurallariyla getirir.
    Task<List<Guid>> GetActiveIdsAsync(Guid feedId, DateOnly date, CancellationToken cancellationToken = default);
}
