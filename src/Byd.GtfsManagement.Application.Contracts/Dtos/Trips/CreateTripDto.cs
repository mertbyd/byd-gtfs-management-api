using System;

namespace Byd.GtfsManagement.Dtos.Trips;

// mirastan gelen: trips.txt kaydinin tum duzenlenebilir alanlari.
// burada yazilan: yalnizca kaydin yazilacagi Feed.
public sealed class CreateTripDto : TripInputDto
{
    public Guid FeedId { get; set; }
}
