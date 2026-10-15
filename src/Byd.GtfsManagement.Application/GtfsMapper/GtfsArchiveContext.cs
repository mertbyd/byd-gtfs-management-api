using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Extensions.Gtfs;
using Byd.GtfsManagement.Feeds;
using Byd.GtfsManagement.Models.Feeds;
using Volo.Abp;

namespace Byd.GtfsManagement.GtfsMapper;

// islevi: Bir arsiv calismasinin Feed'ini, lookup kod-kimlik cevirisini ve disa aktarimda toplanan GTFS kimliklerini tasir.
// sistemdeki gorevi: Her ice veya disa aktarim icin bir kez kurulur ve tum tablolar arasinda paylasilir; lookup tablolari bir kez
// okunur, satir basina veritabanina gidilmez.
public sealed class GtfsArchiveContext(Feed feed, IReadOnlyCollection<GtfsLookupCodeModel> codes)
{
    private readonly Dictionary<(string, string?), int> lookupIds = codes.ToDictionary(
        code => (code.TableName, Text(code.Code)), code => code.Id);

    private readonly Dictionary<(string, int), string?> lookupCodes = codes.ToDictionary(
        code => (code.TableName, code.Id), code => Text(code.Code));

    private readonly Dictionary<Guid, string?> gtfsIds = [];

    // Kayitlarin yazildigi veya okundugu Feed.
    internal Feed Feed => feed;

    // Disa aktarimda, bu kayda bakan yabanci anahtarlarin yazacagi GTFS kimligini saklar.
    internal void CollectId(Guid id, string? gtfsId) => gtfsIds[id] = gtfsId;

    // GTFS anahtarindan Feed kapsamli entity kimligini uretir.
    internal Guid ToId(GtfsKey key) => key.ToGuid(feed.Id);

    // Entity kimliginden orijinal GTFS anahtarina doner.
    internal string ToGtfsId(Guid id) => gtfsIds[id] ?? string.Empty;

    // Lookup kodunun kimligini verir; arsivde tanimsiz kod ice aktarimi geri aldiran is hatasidir.
    internal int ToLookupId(GtfsKey code)
        => lookupIds.TryGetValue((code.Target, code.Value), out var id)
            ? id
            : throw new BusinessException(FeedExceptionCodes.LookupCodeUnknown)
                .WithData(FeedConsts.LookupDataKey, code.Target)
                .WithData(FeedConsts.CodeDataKey, code.Value);

    // Lookup kimliginin GTFS kodunu verir.
    internal string ToLookupCode(string target, int id) => lookupCodes.GetValueOrDefault((target, id)) ?? string.Empty;

    // Sayisal degeri kulturden bagimsiz CSV metnine cevirir.
    private static string? Text(int? value) => value?.ToString(CultureInfo.InvariantCulture);
}
