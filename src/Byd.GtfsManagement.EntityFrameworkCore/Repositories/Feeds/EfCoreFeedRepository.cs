using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Byd.CoreExtensions.Consts;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Repositories;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Entities.CalendarDates;
using Byd.GtfsManagement.Entities.Calendars;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Entities.Services;
using Byd.GtfsManagement.Entities.Shapes;
using Byd.GtfsManagement.Entities.StopTimes;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Models.Feeds;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Feed repository sozlesmesini core EF tabanina baglar; lookup kodlarini okur, Feed'e bagli GTFS kayitlarini toplu yazar
// ve akis halinde okur.
// sistemdeki gorevi: Core'un tenant ve aktiflik filtrelerini kullanir. Toplu yazim change tracker'i atladigi icin ABP'nin
// eklemede bastigi alanlari kendisi basar; kaydin kendisinden kaynaklanan veritabani hatasini (uzunluk, bos zorunlu alan,
// kisit) tekrar denenmeyecek is hatasina cevirir.
[ExposeServices(typeof(IFeedRepository))]
public class EfCoreFeedRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, Feed, Guid>(dbContextProvider), IFeedRepository
{
    private static readonly MethodInfo SetMethod =
        typeof(DbContext).GetMethod(nameof(Microsoft.EntityFrameworkCore.DbContext.Set), Type.EmptyTypes)!;

    private IAuditPropertySetter AuditPropertySetter => LazyServiceProvider.LazyGetRequiredService<IAuditPropertySetter>();

    // Modeldeki lookup tablolarini bulur ve satirlarini tablo adi, kod ve kimlikle dondurur.
    public virtual async Task<List<GtfsLookupCodeModel>> GetLookupCodesAsync(CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        var codes = new List<GtfsLookupCodeModel>();
        foreach (var lookupType in dbContext.Model.GetEntityTypes().Where(type => typeof(ILookup).IsAssignableFrom(type.ClrType)))
        {
            var lookups = await LookupsOf(dbContext, lookupType.ClrType).ToListAsync(cancellationToken);
            codes.AddRange(lookups.Select(lookup => new GtfsLookupCodeModel
            {
                TableName = lookupType.GetTableName()!, Code = lookup.Code, Id = ((IEntity<int>)lookup).Id
            }));
        }
        return codes;
    }

    // Kayitlari damgalayip istegin transaction'i icinde toplu yazar. Kendi tablosuna bakan kayitlar (parent_station) ayni
    // komutta kalir, cunku PostgreSQL yabanci anahtari her komutun sonunda dogrular; digerleri bellegi sinirlamak icin parca
    // parca yazilir.
    public virtual async Task InsertRecordsAsync(
        Feed feed, IEnumerable<IGtfsEntity> records, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        var batch = new List<IGtfsEntity>();
        foreach (var record in records)
        {
            batch.Add(Stamp(record, feed));
            if (batch.Count >= BulkOperationConsts.DefaultBatchSize && !IsSelfReferencing(dbContext, record))
                await InsertBatchAsync(dbContext, batch, cancellationToken);
        }
        await InsertBatchAsync(dbContext, batch, cancellationToken);
    }

    // Feed'in temel GTFS tablolarindaki kayit sayilarini ve takvimlerle tarih istisnalarinin kapsadigi servis araligini okur.
    public virtual async Task<FeedSummaryModel> GetSummaryAsync(Feed feed, CancellationToken cancellationToken = default)
    {
        using var draftRead = DataFilter.Disable<ActiveFeedFilter>();
        var dbContext = await GetDbContextAsync();
        var calendars = dbContext.Set<Calendar>().Where(calendar => calendar.FeedId == feed.Id);
        var calendarDates = dbContext.Set<CalendarDate>().Where(calendarDate => calendarDate.FeedId == feed.Id);
        return new FeedSummaryModel
        {
            Feed = feed,
            AgencyCount = await CountAsync<Agency>(dbContext, feed.Id, cancellationToken),
            StopCount = await CountAsync<Stop>(dbContext, feed.Id, cancellationToken),
            RouteCount = await CountAsync<Route>(dbContext, feed.Id, cancellationToken),
            TripCount = await CountAsync<Trip>(dbContext, feed.Id, cancellationToken),
            StopTimeCount = await CountAsync<StopTime>(dbContext, feed.Id, cancellationToken),
            ServiceCount = await CountAsync<Service>(dbContext, feed.Id, cancellationToken),
            ShapeCount = await dbContext.Set<Shape>().Where(shape => shape.FeedId == feed.Id)
                .Select(shape => shape.GtfsShapeId).Distinct().CountAsync(cancellationToken),
            ServiceStartDate = new[]
            {
                await calendars.MinAsync(calendar => (DateOnly?)calendar.StartDate, cancellationToken),
                await calendarDates.MinAsync(calendarDate => (DateOnly?)calendarDate.Date, cancellationToken)
            }.Min(),
            ServiceEndDate = new[]
            {
                await calendars.MaxAsync(calendar => (DateOnly?)calendar.EndDate, cancellationToken),
                await calendarDates.MaxAsync(calendarDate => (DateOnly?)calendarDate.Date, cancellationToken)
            }.Max()
        };
    }

    // Tablonun Feed'e ait kayitlarini izlemeden okur; buyuk tablolar (stop_times) bellege toplanmaz.
    public virtual async IAsyncEnumerable<TEntity> GetRecordsAsync<TEntity>(
        Guid feedId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where TEntity : class, IGtfsEntity
    {
        using var draftRead = DataFilter.Disable<ActiveFeedFilter>();
        var dbContext = await GetDbContextAsync();
        var records = dbContext.Set<TEntity>().AsNoTracking().Where(record => record.FeedId == feedId);
        await foreach (var record in records.AsAsyncEnumerable().WithCancellation(cancellationToken))
            yield return record;
    }

    // Kimligi bos kayda yeni kimlik, her kayda Feed'in kapsamini ve ekleme audit alanlarini basar.
    private IGtfsEntity Stamp(IGtfsEntity record, Feed feed)
    {
        if (record.Id == Guid.Empty)
            EntityHelper.TrySetId(record, GuidGenerator.Create);
        ObjectHelper.TrySetProperty(record, entity => entity.FeedId, () => feed.Id);
        AuditPropertySetter.SetCreationProperties(record);
        return record;
    }

    // Biriken kayitlari tek COPY ile yazip listeyi bosaltir; veritabaninin reddettigi degeri is hatasina cevirir.
    private static async Task InsertBatchAsync(
        DbContext dbContext, List<IGtfsEntity> batch, CancellationToken cancellationToken = default)
    {
        if (batch.Count == 0)
            return;
        try
        {
            await dbContext.BulkInsertAsync(batch, type: batch[0].GetType(), cancellationToken: cancellationToken);
        }
        catch (PostgresException exception) when (IsRecordRejection(exception))
        {
            throw new BusinessException(FeedExceptionCodes.RecordRejected, innerException: exception);
        }
        batch.Clear();
    }

    // Kaydin tablosunun kendine bakan bir yabanci anahtari olup olmadigini EF modelinden okur.
    private static bool IsSelfReferencing(DbContext dbContext, IGtfsEntity record)
        => dbContext.Model.FindEntityType(record.GetType())!.GetForeignKeys()
            .Any(foreignKey => foreignKey.PrincipalEntityType == foreignKey.DeclaringEntityType);

    // Feed'e bagli tek GTFS tablosunun kayit sayisini verir.
    private static Task<int> CountAsync<TEntity>(DbContext dbContext, Guid feedId, CancellationToken cancellationToken = default)
        where TEntity : class, IGtfsEntity
        => dbContext.Set<TEntity>().CountAsync(entity => entity.FeedId == feedId, cancellationToken);

    // Calisma aninda verilen lookup turunun sorgusunu kurar.
    private static IQueryable<ILookup> LookupsOf(DbContext dbContext, Type lookupType)
        => (IQueryable<ILookup>)SetMethod.MakeGenericMethod(lookupType).Invoke(dbContext, null)!;

    // Hatanin baglanti degil kaydin degeri yuzunden oldugunu bildirir; boyle bir hata her denemede tekrarlanir.
    private static bool IsRecordRejection(PostgresException exception)
        => exception.SqlState is PostgresErrorCodes.StringDataRightTruncation or PostgresErrorCodes.NumericValueOutOfRange
            or PostgresErrorCodes.NotNullViolation or PostgresErrorCodes.ForeignKeyViolation
            or PostgresErrorCodes.UniqueViolation or PostgresErrorCodes.CheckViolation;
}
