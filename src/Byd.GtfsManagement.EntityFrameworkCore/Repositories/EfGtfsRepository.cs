using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: GTFS kayit sorgularina surum adini besleyen ortak navigation'i ekler.
// sistemdeki gorevi: Core repository davranisini devralir; GTFS'e ozel surum cevabi repository'lerde tekrarlanmaz.
public abstract class EfGtfsRepository<TEntity>(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, TEntity, Guid>(dbContextProvider)
    where TEntity : class, IGtfsEntity
{
    // Entity cevaplari surum adi tasir; projeksiyon ve sayimlarda EF gereksiz Include'u kaldirir.
    public override async Task<IQueryable<TEntity>> GetQueryableAsync()
        => (await base.GetQueryableAsync()).Include(entity => entity.Feed);
}
