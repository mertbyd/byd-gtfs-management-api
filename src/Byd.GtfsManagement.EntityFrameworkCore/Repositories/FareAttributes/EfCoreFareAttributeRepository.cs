using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.FareAttributes;
using Byd.GtfsManagement.Interfaces.FareAttributes;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareAttribute repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Cevap icin bagli ajansin GTFS kimligini ve adini yukler.
/// <summary>GTFS ucret ozelligi EF sorgularini uygular.</summary>
[ExposeServices(typeof(IFareAttributeRepository))]
public class EfCoreFareAttributeRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<FareAttribute>(dbContextProvider), IFareAttributeRepository
{
    // Tek kayit okumalarinda ajans bagini yukler.
    public override async Task<IQueryable<FareAttribute>> WithDetailsAsync()
        => IncludeReferences(await GetQueryableAsync());

    // Filtreli listede ayni bagi sorguya ekler.
    protected override async Task<IQueryable<FareAttribute>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => IncludeReferences(await base.CreateFilteredQueryAsync(pagingProperty));

    // Cevap sozlesmesinin kullandigi ajans alanlarini yukler.
    private static IQueryable<FareAttribute> IncludeReferences(IQueryable<FareAttribute> query)
        => query.Include(value => value.Agency);
}
