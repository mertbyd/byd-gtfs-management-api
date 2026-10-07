using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Byd.GtfsManagement.EntityFrameworkCore;

public static class GtfsManagementDbContextModelCreatingExtensions
{
    public static void ConfigureGtfsManagement(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
