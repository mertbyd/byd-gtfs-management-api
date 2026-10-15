using Byd.CoreExtensions.Querying;

namespace Byd.GtfsManagement.Interfaces;

// islevi: Yonetim listelerinin surum kapsamli DataTable filtre alanini bildirir.
// sistemdeki gorevi: Domain kapsam kuralini transport DTO'suna baglamadan ortak filtre sozlesmesiyle yurutur.
public interface IFeedFilter
{
    DataTableFilterItem? FeedId { get; set; }
}
