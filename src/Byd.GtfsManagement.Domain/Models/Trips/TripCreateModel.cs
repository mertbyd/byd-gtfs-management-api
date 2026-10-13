using System;

namespace Byd.GtfsManagement.Models.Trips;

// mirastan gelen: trips.txt kaydinin tum duzenlenebilir alanlari.
// burada yazilan: yalnizca kaydin yazilacagi Feed.
public sealed class TripCreateModel : TripInputModel
{
    public Guid FeedId { get; set; }
}
