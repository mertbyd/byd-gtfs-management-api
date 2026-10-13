using System;

namespace Byd.GtfsManagement.Dtos.Agencies;

// mirastan gelen: CemvSupportId, GtfsAgencyId, Name, Url, Timezone, Language, Phone, FareUrl, Email.
// burada yazilan: yalnizca kaydin yazilacagi Feed.
public sealed class CreateAgencyDto : AgencyInputDto
{
    public Guid FeedId { get; set; }
}
