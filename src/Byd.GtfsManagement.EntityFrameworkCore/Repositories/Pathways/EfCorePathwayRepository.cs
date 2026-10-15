using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Pathways;
using Byd.GtfsManagement.Interfaces.Pathways;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Pathway repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Cevap icin iki bagli duragin GTFS kimliklerini ve adlarini yukler.
/// <summary>GTFS yuruyus yolu EF sorgularini uygular.</summary>
[ExposeServices(typeof(IPathwayRepository))]
public class EfCorePathwayRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<Pathway>(dbContextProvider), IPathwayRepository
{
    // Tek kayit okumalarinda durak baglarini yukler.
    public override async Task<IQueryable<Pathway>> WithDetailsAsync()
        => IncludeReferences(await GetQueryableAsync());

    // Filtreli listede ayni baglari sorguya ekler.
    protected override async Task<IQueryable<Pathway>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => IncludeReferences(await base.CreateFilteredQueryAsync(pagingProperty));

    // Cevap sozlesmesinin kullandigi durak alanlarini yukler.
    private static IQueryable<Pathway> IncludeReferences(IQueryable<Pathway> query)
        => query
            .Include(value => value.FromStop)
            .Include(value => value.ToStop);
}
