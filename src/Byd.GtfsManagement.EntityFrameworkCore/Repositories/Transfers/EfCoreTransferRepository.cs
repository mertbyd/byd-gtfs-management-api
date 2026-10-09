using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Transfers;
using Byd.GtfsManagement.Interfaces.Transfers;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Transfer repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Cevap icin altı bagin GTFS kimliklerini ayni sorguda yukler.
/// <summary>GTFS aktarma EF sorgularini uygular.</summary>
[ExposeServices(typeof(ITransferRepository))]
public class EfCoreTransferRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, Transfer, Guid>(dbContextProvider), ITransferRepository
{
    // Tek kayit okumalarinda aktarma baglarini yukler.
    public override async Task<IQueryable<Transfer>> WithDetailsAsync()
        => IncludeReferences(await GetQueryableAsync());

    // Filtreli listede ayni baglari sorguya ekler.
    protected override async Task<IQueryable<Transfer>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => IncludeReferences(await base.CreateFilteredQueryAsync(pagingProperty));

    // Cevap sozlesmesinin kullandigi alti navigation alanini yukler.
    private static IQueryable<Transfer> IncludeReferences(IQueryable<Transfer> query)
        => query
            .Include(value => value.FromStop)
            .Include(value => value.ToStop)
            .Include(value => value.FromRoute)
            .Include(value => value.ToRoute)
            .Include(value => value.FromTrip)
            .Include(value => value.ToTrip);
}
