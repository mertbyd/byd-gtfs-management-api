using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

public class GtfsManagementDbContextFactory
    : CoreExtensionsDesignTimeDbContextFactory<GtfsManagementDbContext>
{
    protected override string ConnectionStringName => GtfsManagementDbProperties.ConnectionStringName;

    protected override GtfsManagementDbContext CreateInstance(
        DbContextOptions<GtfsManagementDbContext> options)
    {
        return new(options);
    }
}
