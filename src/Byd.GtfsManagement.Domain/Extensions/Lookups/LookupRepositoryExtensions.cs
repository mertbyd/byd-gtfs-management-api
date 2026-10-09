using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Interfaces;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Extensions.Lookups;

// islevi: Bir GTFS alanina bagli lookup kodunun tanimli oldugunu dogrular.
// sistemdeki gorevi: Ayni "kod verildiyse var mi" kontrolu alti Manager'da tekrarlandigi icin tek ada tasindi.
public static class LookupRepositoryExtensions
{
    // Zorunlu lookup kodunun tanimli oldugunu dogrular ve kodu geri verir.
    public static async Task<int> EnsureExistsAsync<TLookup>(
        this IRepository<TLookup, int> repository,
        int lookupId,
        CancellationToken cancellationToken = default)
        where TLookup : class, IEntity<int>, ILookup
    {
        var lookup = await repository.FindAsync(lookupId, cancellationToken: cancellationToken);
        if (lookup is null)
            throw new BusinessException(GeneralExceptionCodes.EntityNotFound);
        return lookupId;
    }

    // Istege bagli lookup kodunu dogrular; kod verilmemisse null doner.
    public static async Task<int?> ResolveOptionalAsync<TLookup>(
        this IRepository<TLookup, int> repository,
        int? lookupId,
        CancellationToken cancellationToken = default)
        where TLookup : class, IEntity<int>, ILookup
    {
        if (lookupId is null)
            return null;
        return await repository.EnsureExistsAsync(lookupId.Value, cancellationToken);
    }
}
