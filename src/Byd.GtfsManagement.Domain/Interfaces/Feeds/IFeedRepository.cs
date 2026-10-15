using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Models.Feeds;

namespace Byd.GtfsManagement.Interfaces.Feeds;

// islevi: Feed icin core repository sozlesmesini, lookup kodlarinin okunmasini, Feed'e bagli GTFS kayitlarinin toplu yazimini
// ve akis halinde okunmasini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir; ice aktarim her GTFS tablosunu tek sozlesmeden yazar, disa
// aktarim tek sozlesmeden okur.
public interface IFeedRepository : IExtendedRepository<Feed>
{
    // Tum lookup tablolarinin satirlarini tablo adi, GTFS kodu ve kimlikle tek seferde okur.
    Task<List<GtfsLookupCodeModel>> GetLookupCodesAsync(CancellationToken cancellationToken = default);

    // Feed'e bagli GTFS kayitlarini degisiklik takipcisini atlayarak toplu ekler; kimlik, kapsam ve audit alanlarini basar.
    Task InsertRecordsAsync(Feed feed, IEnumerable<IGtfsEntity> records, CancellationToken cancellationToken = default);

    // Feed'in temel GTFS tablolarindaki kayit sayilarini ve servis tarih araligini okur.
    Task<FeedSummaryModel> GetSummaryAsync(Feed feed, CancellationToken cancellationToken = default);
    // Feed'e bagli tek GTFS tablosunun kayitlarini degisiklik takipcisine almadan akis halinde okur.
    IAsyncEnumerable<TEntity> GetRecordsAsync<TEntity>(Guid feedId, CancellationToken cancellationToken = default)
        where TEntity : class, IGtfsEntity;
}
