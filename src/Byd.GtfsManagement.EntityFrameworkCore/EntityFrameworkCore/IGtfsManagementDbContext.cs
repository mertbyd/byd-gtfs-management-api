using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

[ConnectionStringName(GtfsManagementDbProperties.ConnectionStringName)]
public interface IGtfsManagementDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
