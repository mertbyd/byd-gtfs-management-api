using System;
using System.Threading;
using System.Threading.Tasks;

namespace Byd.GtfsManagement.Interfaces;

// islevi: Feed kapsamli bir GTFS kaydinin okuma ve silme guardlarini model tiplerinden bagimsiz olarak bildirir.
// sistemdeki gorevi: Application CRUD tabaninin Manager'dan ihtiyac duydugu tek yuzeydir; create/update model
// tipleri bu sozlesmede yer almadigi icin taban jenerik parametre sayisi kucuk kalir.
public interface IGtfsScopedManager<TEntity>
{
    // Kaydi getirip istegin tenant kapsaminda oldugunu dogrular.
    Task<TEntity> EnsureExistsInScopeAsync(Guid id, CancellationToken cancellationToken = default);

    // Kaydin baska GTFS satirlarinca kullanilmadigini dogrular.
    Task EnsureCanDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
