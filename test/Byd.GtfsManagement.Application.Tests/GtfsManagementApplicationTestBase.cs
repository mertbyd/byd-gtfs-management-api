using Volo.Abp.Modularity;

namespace Byd.GtfsManagement;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class GtfsManagementApplicationTestBase<TStartupModule> : GtfsManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
