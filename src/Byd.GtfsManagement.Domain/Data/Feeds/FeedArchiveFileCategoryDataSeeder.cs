using System.Threading;
using System.Threading.Tasks;
using Byd.FileManagement.Interfaces.FileCategories;
using Byd.FileManagement.Managers.FileCategories;
using Byd.FileManagement.Models.FileCategories;
using Byd.GtfsManagement.Consts;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace Byd.GtfsManagement.Data.Feeds;

/// <summary>
/// GTFS ZIP'lerinin file modulundeki kategorisini idempotent olarak olusturur.
/// </summary>
// islevi: Kategori file modulunde yoksa file modulunun Manager'i ile kurar ve repository uzerinden kalici hale getirir.
// sistemdeki gorevi: File modulu kategori seed etmez, kategori listesinin sahibi tuketicidir; GTFS yuklemesi bu kategori
// olmadan calismaz. Mevcut kategoriye dokunmaz.
public class FeedArchiveFileCategoryDataSeeder(
    FileCategoryManager manager,
    IFileCategoryRepository repository) : IDataSeedContributor, ITransientDependency
{
    // Kategori yoksa sabitteki adla ekler; varsa hicbir sey yapmaz.
    [UnitOfWork]
    public async Task SeedAsync(DataSeedContext context)
    {
        var current = await repository.FindAsync(
            entity => entity.Name == FeedConsts.ArchiveFileCategoryName,
            cancellationToken: CancellationToken.None);
        if (current is not null)
            return;

        var model = new FileCategoryCreateModel { Name = FeedConsts.ArchiveFileCategoryName };
        var entity = await manager.CreateAsync(model, CancellationToken.None);
        await repository.InsertAsync(entity, autoSave: true, cancellationToken: CancellationToken.None);
    }
}
