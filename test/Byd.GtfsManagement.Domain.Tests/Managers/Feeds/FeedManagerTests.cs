using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Extensions;
using Byd.CoreExtensions.Interfaces;
using Byd.FileManagement.Managers.FileCategories;
using Byd.GtfsManagement.Consts;
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
using Volo.Abp.MultiTenancy;
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

    // Pasif surum GTFS duzenlemelerine acilmaz; duzenleme yalniz aktif Feed'de yapilir.
    [Fact]
    public async Task EnsureEditableAsync_RejectsInactiveVersion()
    {
        var version = CreateVersion(FeedStatus.Integrated, selected: false);
        var manager = CreateManager(version);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => manager.EnsureEditableAsync(version.Id));

        Assert.Equal(FeedExceptionCodes.InactiveFeedNotEditable, exception.Code);
    }

    // Aktif ve entegre Feed dogrudan duzenlenebilir.
    [Fact]
    public async Task EnsureEditableAsync_AllowsActiveIntegratedFeed()
    {
        var version = CreateVersion(FeedStatus.Integrated, selected: true);
        var manager = CreateManager(version);

        Assert.Same(version, await manager.EnsureEditableAsync(version.Id));
    }

    // Yeni kayit istekten Feed almaz; organizasyonun aktif Feed'ine yazilir.
    [Fact]
    public async Task GetActiveInScopeAsync_ReturnsOrganizationActiveFeed()
    {
        var active = CreateVersion(FeedStatus.Integrated, selected: true);
        var inactive = CreateVersion(FeedStatus.Integrated, selected: false, scope: active);
        var otherOrganization = CreateVersion(FeedStatus.Integrated, selected: true);
        var manager = CreateScopedManager(active, inactive, otherOrganization, active);

        Assert.Same(active, await manager.GetActiveInScopeAsync());
    }

    // Organizasyonun aktif Feed'i yoksa baska organizasyonun aktif Feed'ine yazilmaz.
    [Fact]
    public async Task GetActiveInScopeAsync_RejectsOrganizationWithoutActiveFeed()
    {
        var inactive = CreateVersion(FeedStatus.Integrated, selected: false);
        var otherOrganization = CreateVersion(FeedStatus.Integrated, selected: true);
        var manager = CreateScopedManager(inactive, inactive, otherOrganization);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => manager.GetActiveInScopeAsync());

        Assert.Equal(FeedExceptionCodes.ActiveFeedRequired, exception.Code);
    }

    // Tablolari henuz yazilmayan surum aktif olsa da GTFS duzenlemelerine acilmaz.
    [Theory]
    [InlineData(FeedStatus.Uploaded)]
    [InlineData(FeedStatus.ValidationFailed)]
    [InlineData(FeedStatus.Validated)]
    [InlineData(FeedStatus.IntegrationFailed)]
    public async Task EnsureEditableAsync_RejectsVersionBeforeIntegration(FeedStatus status)
    {
        var version = CreateVersion(status, selected: true);
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

    // Aktarilmis Feed'de dogrulamada hata cikaran degisiklik reddedilir; eski rapor yerinde kalir ve hatalar istemciye doner.
    [Fact]
    public void SetValidationReport_RejectsIntegratedChangeWithErrors()
    {
        var version = CreateVersion(FeedStatus.Integrated, selected: true);
        var manager = CreateManager(version);
        var errors = new List<FeedValidationNoticeModel> { new() { Code = "foreign_key_violation", TotalNotices = 2 } };

        var exception = Assert.Throws<BusinessException>(() =>
            manager.SetValidationReport(version, new FeedValidationReportModel { Errors = errors }));

        Assert.Equal(FeedExceptionCodes.ChangeRejectedByValidation, exception.Code);
        Assert.Same(errors, exception.Data[FeedConsts.ErrorsDataKey]);
        Assert.Null(version.ValidationReport);
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

    // Istegin kapsamini verilen Feed'in tenant ve organizasyonuna kurar; repository sorgusunu bellekteki Feed'lerde calistirir.
    private static FeedManager CreateScopedManager(Feed scope, params Feed[] feeds)
    {
        var repository = Substitute.For<IFeedRepository>();
        repository.FindAsync(Arg.Any<Expression<Func<Feed, bool>>>(), Arg.Any<bool>(), Arg.Any<CancellationToken>())
            .Returns(call => feeds.AsQueryable().FirstOrDefault(call.Arg<Expression<Func<Feed, bool>>>()));
        var tenant = Substitute.For<ICurrentTenant>();
        tenant.Id.Returns(scope.TenantId);
        var organization = Substitute.For<ICurrentOrganization>();
        organization.Id.Returns(scope.OrganizationId);
        var lazyServiceProvider = Substitute.For<IAbpLazyServiceProvider>();
        lazyServiceProvider.LazyGetRequiredService<ICurrentTenant>().Returns(tenant);
        return new FeedManager(repository, Substitute.For<IDataFilter>(), null!, organization,
            Substitute.For<ICurrentUser>()) { LazyServiceProvider = lazyServiceProvider };
    }

    // Test verisini public setter eklemeden persistence alanlarina yerlestirir; kapsam verilmezse yeni tenant ve
    // organizasyon acar.
    private static Feed CreateVersion(FeedStatus status, bool selected, Feed? scope = null)
    {
        var version = new Feed(Guid.NewGuid(), scope?.TenantId ?? Guid.NewGuid(), scope?.OrganizationId ?? Guid.NewGuid(),
            "Surum", FeedSourceType.Upload, null);
        typeof(Feed).GetProperty(nameof(Feed.Status))!.SetValue(version, status);
        typeof(Feed).GetProperty(nameof(Feed.IsActive))!.SetValue(version, selected);
        return version;
    }
}
