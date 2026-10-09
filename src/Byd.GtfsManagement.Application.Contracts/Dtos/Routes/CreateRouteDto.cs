using System;

namespace Byd.GtfsManagement.Dtos.Routes;

// mirastan gelen: routes.txt kaydinin tum duzenlenebilir alanlari.
// burada yazilan: yalnizca kaydin yazilacagi Feed.
public sealed class CreateRouteDto : RouteInputDto
{
    public Guid FeedId { get; set; }
}
