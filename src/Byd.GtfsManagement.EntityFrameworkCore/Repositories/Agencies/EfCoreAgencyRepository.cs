using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Interfaces.Agencies;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Agency repository sozlesmesini core EF tabanina baglar ve cEMV lookup navigasyonunu yukler.
// sistemdeki gorevi: Cevap DTO'sunun lookup adini tasiyabilmesi icin Include'u tek yerde tutar.
[ExposeServices(typeof(IAgencyRepository))]
public class EfCoreAgencyRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, Agency, Guid>(dbContextProvider), IAgencyRepository
{
    // Tekil okumalarda cEMV adinin dolu gelmesini saglar.
    public override async Task<IQueryable<Agency>> WithDetailsAsync()
        => (await GetQueryableAsync()).Include(agency => agency.CemvSupport);

    // Liste sorgusunda da lookup adini yukler; filtre, siralama ve sayfalama tabanda kalir.
    protected override async Task<IQueryable<Agency>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => (await base.CreateFilteredQueryAsync(pagingProperty)).Include(agency => agency.CemvSupport);
}
