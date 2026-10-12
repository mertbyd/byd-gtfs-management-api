using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.EntityFrameworkCore;
using Byd.CoreExtensions.Extensions;
using Volo.Abp.Data;

namespace Byd.GtfsManagement.EntityFrameworkCore;

[ConnectionStringName(GtfsManagementDbProperties.ConnectionStringName)]
public class GtfsManagementDbContext : BydDbContext<GtfsManagementDbContext>, IGtfsManagementDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public GtfsManagementDbContext(DbContextOptions<GtfsManagementDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(GtfsManagementDbContext).Assembly);

        builder.ConfigureGtfsManagement();

        builder.ApplySnakeCaseNaming();
    }
}
