using System;

namespace Byd.GtfsManagement.Dtos.Services;

// mirastan gelen: GtfsServiceId.
// burada yazilan: yalnizca kaydin yazilacagi Feed.
public sealed class CreateServiceDto : ServiceInputDto
{
    public Guid FeedId { get; set; }
}
