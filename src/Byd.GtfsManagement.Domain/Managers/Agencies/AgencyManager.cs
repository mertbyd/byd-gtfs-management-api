using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Entities.Lookup.CemvSupports;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Extensions.Gtfs;
using Byd.GtfsManagement.Extensions.Lookups;
using Byd.GtfsManagement.Interfaces.Agencies;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.Agencies;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Agencies;

// islevi: agency.txt kaydinin alan normalizasyonunu ve GTFS tekillik kurallarini yonetir.
// sistemdeki gorevi: Ajans is kurallarinin tek sahibidir; kapsam ve silme guardlarini tabandan devralir.
public class AgencyManager(
    IAgencyRepository repository,
    FeedManager feedManager,
    IRepository<CemvSupport, int> cemvSupportRepository)
    : GtfsFeedScopedManager<Agency, AgencyCreateModel, AgencyUpdateModel>(repository, feedManager)
{
    // Hedef Feed'i kapsamda dogrulayip yeni ajansi kurar.
    public override async Task<Agency> CreateAsync(
        AgencyCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new Agency(
            GuidGenerator.Create(), feed.TenantId, feed.Id);
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Yuklenmis ajansin duzenlenebilir alanlarini ayni kurallardan gecirerek gunceller.
    public override Task UpdateAsync(
        Agency entity, AgencyUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Duzenlenebilir alanlari normalize eder ve GTFS kurallarini dogrular.
    private async Task ApplyAsync(Agency entity, AgencyInputModel model, CancellationToken cancellationToken = default)
    {
        entity.CemvSupportId = await cemvSupportRepository.ResolveOptionalAsync(
            model.CemvSupportId, cancellationToken);
        entity.GtfsAgencyId = NormalizeOptionalText(model.GtfsAgencyId);
        entity.Name = NormalizeRequiredText(model.Name);
        entity.Url = NormalizeRequiredText(model.Url);
        entity.Timezone = NormalizeRequiredText(model.Timezone)
            .EnsureKnownTimezone(AgencyExceptionCodes.TimezoneInvalid);
        entity.Language = NormalizeOptionalText(model.Language);
        entity.Phone = NormalizeOptionalText(model.Phone);
        entity.FareUrl = NormalizeOptionalText(model.FareUrl);
        entity.Email = NormalizeOptionalText(model.Email);
        await CheckGtfsAgencyIdUniqueAsync(entity, cancellationToken);
    }

    // agency_id dolu ise Feed icinde tekil olmasini, bos ise Feed'de tek ajans bulunmasini dogrular.
    private Task CheckGtfsAgencyIdUniqueAsync(Agency entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsAgencyId = entity.GtfsAgencyId;
        Expression<Func<Agency, bool>> duplicate = gtfsAgencyId is null
            ? agency => agency.FeedId == feedId && agency.GtfsAgencyId == null
            : agency => agency.FeedId == feedId && agency.GtfsAgencyId == gtfsAgencyId;
        return CheckAlreadyExistsAsync(duplicate, entity.Id, cancellationToken);
    }
}
