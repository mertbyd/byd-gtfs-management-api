using Volo.Abp.Modularity;

namespace Byd.GtfsManagement;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class GtfsManagementDomainTestBase<TStartupModule> : GtfsManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
