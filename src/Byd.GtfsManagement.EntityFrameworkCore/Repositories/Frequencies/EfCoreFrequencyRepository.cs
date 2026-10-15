using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Frequencies;
using Byd.GtfsManagement.Interfaces.Frequencies;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Frequency repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Public cevap icin bagli seferin GTFS kimligini yukler.
/// <summary>Sefer sikligi EF sorgularini uygular.</summary>
[ExposeServices(typeof(IFrequencyRepository))]
public class EfCoreFrequencyRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<Frequency>(dbContextProvider),
        IFrequencyRepository
{
    // Tek kayit okumalarinda bagli seferi yukler.
    public override async Task<IQueryable<Frequency>> WithDetailsAsync()
        => (await GetQueryableAsync()).Include(value => value.Trip);

    // Filtreli listede bagli seferin GTFS kimligini yukler.
    protected override async Task<IQueryable<Frequency>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => (await base.CreateFilteredQueryAsync(pagingProperty)).Include(value => value.Trip);
}
