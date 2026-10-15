using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Shapes;
using Byd.GtfsManagement.Interfaces.Shapes;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.Shapes;

namespace Byd.GtfsManagement.Managers.Shapes;

// islevi: shapes.txt sekil kimligi, sira, koordinat ve mesafe kurallarini korur.
// sistemdeki gorevi: Her sekil noktasini Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS sekil is kurallarini yonetir.</summary>
public class ShapeManager(
    IShapeRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<Shape, ShapeCreateModel, ShapeUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni sekil noktasi kaydini kurar.
    public override async Task<Shape> CreateAsync(
        ShapeCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new Shape(
            GuidGenerator.Create(), feed.Id, NormalizeRequiredText(model.GtfsShapeId));
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Mevcut sekil noktasinin koordinat, sira ve mesafesini gunceller.
    public override Task UpdateAsync(
        Shape entity, ShapeUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsShapeId = NormalizeRequiredText(model.GtfsShapeId);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Noktanin alanlarini uygular ve ayni shape_id icinde sira tekilligini dogrular.
    private async Task ApplyAsync(
        Shape entity, ShapeInputModel model, CancellationToken cancellationToken = default)
    {

        entity.Latitude = model.Latitude;
        entity.Longitude = model.Longitude;
        entity.Sequence = model.Sequence;
        entity.DistTraveled = model.DistTraveled;

        await CheckShapePointUniqueAsync(entity, cancellationToken);
    }

    // Ayni Feed ve shape_id altinda ayni sira numarasina sahip nokta olmamasini saglar.
    private Task CheckShapePointUniqueAsync(Shape entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsShapeId = entity.GtfsShapeId;
        var sequence = entity.Sequence;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId
                && value.GtfsShapeId == gtfsShapeId
                && value.Sequence == sequence,
            entity.Id, cancellationToken);
    }
}

