using System;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Extensions;
using Byd.CoreExtensions.Interfaces;
using Byd.FileManagement.Managers.FileCategories;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Extensions.Feeds;
using Byd.GtfsManagement.Feeds;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.Feeds;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Users;
using Xunit;

namespace Byd.GtfsManagement.Managers.Feeds;

// islevi: Yayin seciminin surum duzenleme ve taslak kurallarini korudugunu sinar.
// sistemdeki gorevi: Aktiflik satirlardan kaldirildiginda eski surumun yanlislikla degistirilmesini engelleyen regresyonlari yakalar.
public class FeedManagerTests
{
    [Fact]
    public void DefaultName_ContainsFileDateAndOrganizationWithinLimit()
    {
        var organizationId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var model = new FeedCreateModel { FileName = new string('a', 200) + ".zip" };

        var name = model.CreateDefaultName(organizationId, new DateTime(2026, 10, 14, 10, 30, 0, DateTimeKind.Utc));

        Assert.StartsWith("aaa", name);
        Assert.Contains("-20261014103000", name);
        Assert.EndsWith($"-{organizationId}", name);
        Assert.True(name.Length <= 200);
    }

    // Feed agdan cikarilsa da secili yayin surumu dogrudan duzenlenemez.
    [Fact]
    public async Task EnsureEditableAsync_RejectsSelectedVersionWhenFeedIsDisabled()
    {
        var version = CreateVersion(FeedStatus.Integrated, selected: true);
        var manager = CreateManager(version);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => manager.EnsureEditableAsync(version.Id));

        Assert.Equal(FeedExceptionCodes.ActiveFeedRequiresDraft, exception.Code);
    }

    // Entegre pasif taslak duzenlenebilir; yayin secimi baska surumde kalir.
    [Fact]
    public async Task EnsureEditableAsync_AllowsIntegratedDraft()
    {
        var version = CreateVersion(FeedStatus.Integrated, selected: false);
        var manager = CreateManager(version);

        Assert.Same(version, await manager.EnsureEditableAsync(version.Id));
    }

    // Tablolari henuz yazilmayan surum GTFS duzenlemelerine acilmaz.
    [Theory]
    [InlineData(FeedStatus.Uploaded)]
    [InlineData(FeedStatus.ValidationFailed)]
    [InlineData(FeedStatus.Validated)]
    [InlineData(FeedStatus.IntegrationFailed)]
    public async Task EnsureEditableAsync_RejectsVersionBeforeIntegration(FeedStatus status)
    {
        var version = CreateVersion(status, selected: false);
        var manager = CreateManager(version);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => manager.EnsureEditableAsync(version.Id));

        Assert.Equal(FeedExceptionCodes.NotIntegrated, exception.Code);
    }

    // Taslak yalniz Feed'in secili ve entegre surumunden kopyalanir.
    [Fact]
    public async Task EnsureDraftSourceAsync_RejectsUnselectedVersion()
    {
        var version = CreateVersion(FeedStatus.Integrated, selected: false);
        var manager = CreateManager(version);

        var exception = await Assert.ThrowsAsync<BusinessException>(() =>
            manager.EnsureDraftSourceAsync(version.Id));

        Assert.Equal(FeedExceptionCodes.StatusTransitionInvalid, exception.Code);
    }

    // Basarili dogrulama entegrasyonu kuyruga alir ve eski hata kodunu temizler.
    [Fact]
    public void SetValidationReport_QueuesValidatedVersionWithoutActivatingFeed()
    {
        var version = CreateVersion(FeedStatus.IntegrationFailed, selected: false);
        typeof(Feed).GetProperty(nameof(Feed.FailureCode))!.SetValue(version, "previous-failure");
        var manager = CreateManager(version);

        manager.SetValidationReport(version, new FeedValidationReportModel());

        Assert.Equal(FeedStatus.Validated, version.Status);
        Assert.Null(version.FailureCode);
        Assert.False(version.IsActive);
    }

    // Hata raporu satirlarin yazilmasina izin vermez.
    [Fact]
    public void SetValidationReport_KeepsInvalidVersionOutOfIntegrationQueue()
    {
        var version = CreateVersion(FeedStatus.Uploaded, selected: false);
        var manager = CreateManager(version);

        manager.SetValidationReport(version, new FeedValidationReportModel
        {
            Errors = [new FeedValidationNoticeModel { Code = "missing_required_file", TotalNotices = 1 }]
        });

        Assert.Equal(FeedStatus.ValidationFailed, version.Status);
    }

    // Entegre verinin yeniden dogrulanmasi ikinci bir tablo entegrasyonu olusturmaz.
    [Fact]
    public void SetValidationReport_DoesNotRequeueIntegratedVersion()
    {
        var version = CreateVersion(FeedStatus.Integrated, selected: false);
        var manager = CreateManager(version);

        manager.SetValidationReport(version, new FeedValidationReportModel());

        Assert.Equal(FeedStatus.Integrated, version.Status);
    }

    // Her durumdan Integrated atamaya izin verilmez; hatali olay sirasi gorunur kalir.
    [Fact]
    public void SetIntegrated_RejectsUnvalidatedVersion()
    {
        var version = CreateVersion(FeedStatus.Uploaded, selected: false);
        var manager = CreateManager(version);

        var exception = Assert.Throws<BusinessException>(() => manager.SetIntegrated(version));

        Assert.Equal(FeedExceptionCodes.StatusTransitionInvalid, exception.Code);
    }

    // Kapsam yuklemesini ayirarak gercek Manager'in surum kurallarini calistirir.
    private static FeedManager CreateManager(Feed version)
    {
        var manager = Substitute.ForPartsOf<FeedManager>(
            Substitute.For<IFeedRepository>(), Substitute.For<IDataFilter>(),
            (FileCategoryManager)null!, Substitute.For<ICurrentOrganization>(), Substitute.For<ICurrentUser>());
        manager.Configure().EnsureExistsIncludingInactiveInScopeAsync(version.Id, Arg.Any<CancellationToken>())
            .Returns(version);
        return manager;
    }

    // Test verisini public setter eklemeden persistence alanlarina yerlestirir.
    private static Feed CreateVersion(FeedStatus status, bool selected)
    {
        var tenantId = Guid.NewGuid();
        var version = new Feed(Guid.NewGuid(), tenantId, Guid.NewGuid(), "Surum", FeedSourceType.Upload, null);
        typeof(Feed).GetProperty(nameof(Feed.Status))!.SetValue(version, status);
        typeof(Feed).GetProperty(nameof(Feed.IsActive))!.SetValue(version, selected);
        return version;
    }
}
