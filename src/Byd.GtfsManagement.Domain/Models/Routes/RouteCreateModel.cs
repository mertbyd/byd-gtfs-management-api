using System;

namespace Byd.GtfsManagement.Models.Routes;

// mirastan gelen: routes.txt kaydinin tum duzenlenebilir alanlari.
// burada yazilan: yalnizca kaydin yazilacagi Feed.
public sealed class RouteCreateModel : RouteInputModel
{
    public Guid FeedId { get; set; }
}
