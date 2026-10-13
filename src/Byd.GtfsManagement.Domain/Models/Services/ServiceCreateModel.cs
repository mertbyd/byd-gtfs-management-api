using System;

namespace Byd.GtfsManagement.Models.Services;

// mirastan gelen: GtfsServiceId.
// burada yazilan: yalnizca kaydin yazilacagi Feed.
public sealed class ServiceCreateModel : ServiceInputModel
{
    public Guid FeedId { get; set; }
}
