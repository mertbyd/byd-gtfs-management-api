using System;

namespace Byd.GtfsManagement.Dtos.Stops;

// mirastan gelen: stops.txt kaydinin tum duzenlenebilir alanlari.
// burada yazilan: yalnizca kaydin yazilacagi Feed.
public sealed class CreateStopDto : StopInputDto
{
    public Guid FeedId { get; set; }
}
