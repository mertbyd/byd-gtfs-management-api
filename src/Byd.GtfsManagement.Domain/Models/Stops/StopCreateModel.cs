using System;

namespace Byd.GtfsManagement.Models.Stops;

// mirastan gelen: stops.txt kaydinin tum duzenlenebilir alanlari.
// burada yazilan: yalnizca kaydin yazilacagi Feed.
public sealed class StopCreateModel : StopInputModel
{
    public Guid FeedId { get; set; }
}
