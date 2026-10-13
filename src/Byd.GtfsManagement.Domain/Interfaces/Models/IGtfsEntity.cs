using Byd.GtfsManagement.Entities.Feeds;
using Volo.Abp.MultiTenancy;

namespace Byd.GtfsManagement.Interfaces;

// islevi: Feed kapsamli tum GTFS varliklarinin ortak sozlesmesini tanimlar.
// sistemdeki gorevi: Tenant ve Feed kimlikleri ile Feed navigasyonunu tek sozlesmede birlestirir.
public interface IGtfsEntity : IMultiTenant, IHasFeed
{
    // Kaydin bagli oldugu GTFS Feed navigasyonu.
    Feed Feed { get; }
}
