using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Attributions;
using Byd.GtfsManagement.Interfaces.Attributions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Attribution repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Cevap icin bagli ajans, guzergah ve sefer verilerini yukler.
/// <summary>GTFS atif EF sorgularini uygular.</summary>
[ExposeServices(typeof(IAttributionRepository))]
public class EfCoreAttributionRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, Attribution, Guid>(dbContextProvider), IAttributionRepository
{
    // Tek kayit okumalarinda ajans, guzergah ve sefer baglarini yukler.
    public override async Task<IQueryable<Attribution>> WithDetailsAsync()
        => IncludeReferences(await GetQueryableAsync());

    // Filtreli listede ayni baglari sorguya ekler.
    protected override async Task<IQueryable<Attribution>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => IncludeReferences(await base.CreateFilteredQueryAsync(pagingProperty));

    // Cevap sozlesmesinin kullandigi alanlari yukler.
    private static IQueryable<Attribution> IncludeReferences(IQueryable<Attribution> query)
        => query
            .Include(value => value.Agency)
            .Include(value => value.Route)
            .Include(value => value.Trip);
}
