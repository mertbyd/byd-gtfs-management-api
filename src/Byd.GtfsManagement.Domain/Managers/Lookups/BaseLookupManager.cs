using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Byd.GtfsManagement.Managers.Lookups;

// islevi: Sabit tanimli GTFS lookup tablolarinin eksik satirlarini belirleyen ortak akisi saglar.
// sistemdeki gorevi: Her lookup manager'in ayni CreateMissingAsync sablonunu yinelemesini onler.
public abstract class BaseLookupManager<TEntity>(IRepository<TEntity, int> repository) : DomainService
    where TEntity : Entity<int>, ILookup
{
    // Alt sinifin Domain.Shared icindeki sabit tanimlarini dondurur.
    protected abstract IReadOnlyList<(int Id, string Name, int Code)> GetDefinitions();

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    protected abstract TEntity CreateEntity(int id, string name, int code);

    // Sadece henuz bulunmayan sabit kimlikli GTFS kodlarini kurar.
    public virtual async Task<List<TEntity>> CreateMissingAsync(CancellationToken cancellationToken = default)
    {
        var existingIds = (await repository.GetListAsync(cancellationToken: cancellationToken))
            .Select(value => value.Id).ToHashSet();
        return GetDefinitions().Where(value => !existingIds.Contains(value.Id))
            .Select(value => CreateEntity(value.Id, value.Name, value.Code)).ToList();
    }
}
