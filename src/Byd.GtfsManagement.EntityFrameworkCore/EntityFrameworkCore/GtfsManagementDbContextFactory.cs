using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Tasarim zamaninda migration araclari icin GTFS DbContext'ini kurar.
// sistemdeki gorevi: Baglanti dizesini core'un tasarim zamani fabrikasindan modulun baglanti adiyla okur.
public class GtfsManagementDbContextFactory
    : CoreExtensionsDesignTimeDbContextFactory<GtfsManagementDbContext>
{
    protected override string ConnectionStringName => GtfsManagementDbProperties.ConnectionStringName;

    // Verilen seceneklerle DbContext ornegini kurar.
    protected override GtfsManagementDbContext CreateInstance(
        DbContextOptions<GtfsManagementDbContext> options)
    {
        return new(options);
    }
}
