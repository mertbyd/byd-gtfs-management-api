using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Managers;
using Byd.FileManagement.Entities.FileCategories;
using Byd.FileManagement.Managers.FileCategories;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Extensions.Feeds;
using Byd.GtfsManagement.Feeds;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Models.Feeds;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace Byd.GtfsManagement.Managers.Feeds;

// Owns organization scoping, the one-active-feed rule, and import state transitions.
public class FeedManager(
    IFeedRepository repository,
    IDataFilter dataFilter,
    FileCategoryManager fileCategoryManager,
    ICurrentOrganization currentOrganization,
    ICurrentUser currentUser)
    : BaseManager<Feed, Guid>(repository)
{
    public virtual async Task<Feed> CreateAsync(FeedCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        EnsureScopeAvailable();
        var name = string.IsNullOrWhiteSpace(model.Name)
            ? model.CreateDefaultName(currentOrganization.Id!.Value, Clock.Now)
            : NormalizeRequiredText(model.Name);
        using var inactiveIncluded = dataFilter.Disable<ActiveFeedFilter>();
        await CheckAlreadyExistsAsync(feed => feed.OrganizationId == currentOrganization.Id && feed.Name == name,
            cancellationToken);
        return new Feed(GuidGenerator.Create(), CurrentTenant.Id!.Value, currentOrganization.Id!.Value,
            name, model.SourceType, NormalizeOptionalText(model.SourceUrl), currentUser.Id);
    }

    public virtual async Task<Feed> EnsureExistsInScopeAsync(Guid id, CancellationToken cancellationToken = default)
    {
        EnsureScopeAvailable();
        var tenantId = CurrentTenant.Id;
        var organizationId = currentOrganization.Id;
        return (await TryGetByAsync(
            feed => feed.Id == id && feed.TenantId == tenantId && feed.OrganizationId == organizationId,
            cancellationToken: cancellationToken))!;
    }

    public virtual async Task<Feed> EnsureExistsIncludingInactiveInScopeAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        using var inactiveIncluded = dataFilter.Disable<ActiveFeedFilter>();
        return await EnsureExistsInScopeAsync(id, cancellationToken);
    }

    public virtual async Task<Feed?> FindActiveAsync(Guid feedId, CancellationToken cancellationToken = default)
    {
        var feed = await EnsureExistsIncludingInactiveInScopeAsync(feedId, cancellationToken);
        return feed.IsActive ? feed : null;
    }

    // Organizasyonun aktif Feed'ini getirir; yeni GTFS kayitlari istekten Feed almadan bu Feed'e yazilir.
    public virtual async Task<Feed> GetActiveInScopeAsync(CancellationToken cancellationToken = default)
    {
        EnsureScopeAvailable();
        return await Repository.FindAsync(feed => feed.TenantId == CurrentTenant.Id
                && feed.OrganizationId == currentOrganization.Id && feed.IsActive, cancellationToken: cancellationToken)
            ?? throw new BusinessException(FeedExceptionCodes.ActiveFeedRequired);
    }

    // GTFS kayitlari yalniz organizasyonun aktif ve tablolari aktarilmis Feed'inde eklenir, guncellenir ve silinir.
    public virtual async Task<Feed> EnsureEditableAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var feed = await EnsureExistsIncludingInactiveInScopeAsync(id, cancellationToken);
        return !feed.IsActive ? throw new BusinessException(FeedExceptionCodes.InactiveFeedNotEditable)
            : feed.Status != FeedStatus.Integrated ? throw new BusinessException(FeedExceptionCodes.NotIntegrated)
            : feed;
    }

    public virtual async Task<string> GetSourceUrlAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var feed = await EnsureExistsIncludingInactiveInScopeAsync(id, cancellationToken);
        return feed.SourceUrl ?? throw new BusinessException(FeedExceptionCodes.SourceNotConfigured);
    }

    public virtual async Task<Feed> ActivateAsync(Feed feed, CancellationToken cancellationToken = default)
    {
        EnsureScopeAvailable();
        if (feed.Status != FeedStatus.Integrated || feed.ValidationReport?.Errors.Count > 0)
            throw new BusinessException(FeedExceptionCodes.NotIntegrated);
        using var inactiveIncluded = dataFilter.Disable<ActiveFeedFilter>();
        var active = await Repository.GetListAsync(candidate =>
            candidate.OrganizationId == currentOrganization.Id && candidate.TenantId == CurrentTenant.Id
            && candidate.IsActive && candidate.Id != feed.Id, cancellationToken: cancellationToken);
        foreach (var previous in active)
        {
            previous.IsActive = false;
            await Repository.UpdateAsync(previous, autoSave: true, cancellationToken: cancellationToken);
        }
        feed.IsActive = true;
        await Repository.UpdateAsync(feed, cancellationToken: cancellationToken);
        return feed;
    }

    public virtual async Task<FileCategory> GetArchiveCategoryAsync(CancellationToken cancellationToken = default)
        => (await fileCategoryManager.TryGetByAsync(
            category => category.Name == FeedConsts.ArchiveFileCategoryName,
            cancellationToken: cancellationToken))!;

    public virtual void AttachStoredFile(Feed feed, Guid storedFileId, string storedFileName)
    {
        EnsureStatus(feed, FeedStatus.Uploaded);
        feed.StoredFileId = storedFileId;
        feed.StoredFileName = storedFileName;
    }

    public virtual void SetValidationReport(Feed feed, FeedValidationReportModel report)
    {
        ArgumentNullException.ThrowIfNull(report);
        EnsureIntegratedDataValid(feed, report);
        feed.ValidationReport = report;
        if (feed.Status != FeedStatus.Integrated)
        {
            feed.FailureCode = null;
            feed.Status = report.Errors.Count == 0 ? FeedStatus.Validated : FeedStatus.ValidationFailed;
        }
    }

    public virtual async Task<List<Feed>> GetWaitingForIntegrationAsync(CancellationToken cancellationToken = default)
    {
        using var allTenants = dataFilter.Disable<IMultiTenant>();
        using var allOrganizations = dataFilter.Disable<IMultiOrganization>();
        using var inactiveIncluded = dataFilter.Disable<ActiveFeedFilter>();
        return await Repository.GetListAsync(
            feed => feed.Status == FeedStatus.Validated, cancellationToken: cancellationToken);
    }

    public virtual async Task<Feed?> FindWaitingForIntegrationAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        EnsureScopeAvailable();
        using var inactiveIncluded = dataFilter.Disable<ActiveFeedFilter>();
        return await Repository.FindAsync(feed => feed.Id == id && feed.TenantId == CurrentTenant.Id
            && feed.OrganizationId == currentOrganization.Id && feed.Status == FeedStatus.Validated,
            cancellationToken: cancellationToken);
    }

    public virtual void SetIntegrated(Feed feed)
    {
        EnsureStatus(feed, FeedStatus.Validated);
        feed.Status = FeedStatus.Integrated;
        feed.FailureCode = null;
    }

    public virtual void SetIntegrationFailed(Feed feed, string? failureCode)
    {
        EnsureStatus(feed, FeedStatus.Validated);
        feed.Status = FeedStatus.IntegrationFailed;
        feed.FailureCode = failureCode;
    }

    public virtual void SetOperationUser(Feed feed)
        => feed.LastOperationUserId = currentUser.Id ?? feed.LastOperationUserId;

    public virtual void EnsureScopeAvailable()
    {
        if (CurrentTenant.Id is null || currentOrganization.Id is null)
            throw new BusinessException(FeedExceptionCodes.ScopeRequired);
    }

    // Aktarilmis Feed'in tablolari yayindaki veridir; dogrulamada hata cikaran degisiklik yazilmaz. Hatalar istemciye doner ve
    // degisiklik raporla birlikte geri alinir.
    private static void EnsureIntegratedDataValid(Feed feed, FeedValidationReportModel report)
    {
        if (feed.Status == FeedStatus.Integrated && report.Errors.Count > 0)
            throw new BusinessException(FeedExceptionCodes.ChangeRejectedByValidation)
                .WithData(FeedConsts.ErrorCountDataKey, report.Errors.Count)
                .WithData(FeedConsts.ErrorsDataKey, report.Errors);
    }

    private static void EnsureStatus(Feed feed, FeedStatus expected)
    {
        if (feed.Status != expected)
            throw new BusinessException(FeedExceptionCodes.StatusTransitionInvalid);
    }

}
