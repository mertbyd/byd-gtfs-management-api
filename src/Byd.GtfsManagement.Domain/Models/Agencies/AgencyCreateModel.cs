using System;

namespace Byd.GtfsManagement.Models.Agencies;

// mirastan gelen: CemvSupportId, GtfsAgencyId, Name, Url, Timezone, Language, Phone, FareUrl, Email.
// burada yazilan: yalnizca kaydin yazilacagi Feed.
public sealed class AgencyCreateModel : AgencyInputModel
{
    public Guid FeedId { get; set; }
}
