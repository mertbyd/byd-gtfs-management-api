using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.FareRules;
using Byd.GtfsManagement.Interfaces.FareRules;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareRule repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Cevap icin bagli ucret taniminin ve guzergahin verilerini yukler.
/// <summary>GTFS ucret kurali EF sorgularini uygular.</summary>
[ExposeServices(typeof(IFareRuleRepository))]
public class EfCoreFareRuleRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, FareRule, Guid>(dbContextProvider), IFareRuleRepository
{
    // Tek kayit okumalarinda ucret ve guzergah baglarini yukler.
    public override async Task<IQueryable<FareRule>> WithDetailsAsync()
        => IncludeReferences(await GetQueryableAsync());

    // Filtreli listede ayni baglari sorguya ekler.
    protected override async Task<IQueryable<FareRule>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => IncludeReferences(await base.CreateFilteredQueryAsync(pagingProperty));

    // Cevap sozlesmesinin kullandigi alanlari yukler.
    private static IQueryable<FareRule> IncludeReferences(IQueryable<FareRule> query)
        => query
            .Include(value => value.FareAttribute)
            .Include(value => value.Route);
}
