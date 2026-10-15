using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.ExceptionCodes;
using Volo.Abp;

namespace Byd.GtfsManagement.GtfsMapper;

// islevi: GTFS dosyasi ile DTO arasinda iki yonlu esler; dosya verilirse satirlari okur, kayit verilirse dosyaya yazar.
// sistemdeki gorevi: CSV seviyesinin tek yeridir; kolon adlarini DTO'daki [Name] beyanlarindan alir, entity, repository, Feed ve
// tablo adi bilmez. Bozuk deger dosya, satir ve alan bilgili is hatasi verir.
internal static class GtfsCsvMapper
{
    // Dosya -> DTO: satirlari akis halinde okur; arsivde olmayan dosya bos sonuc verir.
    internal static IEnumerable<TDto> Map<TDto>(ZipArchiveEntry? file, GtfsKeyConverter keys)
    {
        if (file is null)
            yield break;
        using var csv = new CsvReader(new StreamReader(file.Open()), CreateConfiguration(file.FullName));
        Configure(csv.Context);
        csv.Context.RegisterClassMap(ColumnsOf<TDto>(csv.Context, keys));
        foreach (var record in csv.GetRecords<TDto>())
            yield return record;
    }

    // DTO -> dosya: akisi bellekte biriktirmeden yazar; kaydi olmayan tablo icin dosya acmaz.
    internal static async Task Map<TDto>(IAsyncEnumerable<TDto> records, ZipArchive zip, string fileName,
        GtfsKeyConverter keys, CancellationToken cancellationToken = default)
    {
        await using var rows = records.GetAsyncEnumerator(cancellationToken);
        if (!await rows.MoveNextAsync())
            return;
        await using var csv = new CsvWriter(
            new StreamWriter(zip.CreateEntry(fileName).Open()), CreateConfiguration(fileName));
        Configure(csv.Context);
        csv.Context.RegisterClassMap(WithoutRepeatedColumns(ColumnsOf<TDto>(csv.Context, keys)));
        csv.WriteHeader<TDto>();
        do
        {
            await csv.NextRecordAsync();
            csv.WriteRecord(rows.Current);
        } while (await rows.MoveNextAsync());
    }

    // Tum GTFS dosyalarinin hucre kurallarini tek yerde kurar; DTO'lara kolon basina attribute gerekmez.
    private static void Configure(CsvContext context)
    {
        context.TypeConverterOptionsCache.GetOptions<string>().NullValues.Add(string.Empty);
        context.TypeConverterOptionsCache.GetOptions<DateOnly>().Formats = [GtfsTimeConsts.DateFormat];
        context.TypeConverterOptionsCache.GetOptions<DateOnly?>().Formats = [GtfsTimeConsts.DateFormat];
        var booleans = context.TypeConverterOptionsCache.GetOptions<bool>();
        booleans.BooleanTrueValues.Add(FeedConsts.BooleanTrueValue);
        booleans.BooleanFalseValues.Add(FeedConsts.BooleanFalseValue);
    }

    // Kolonlari DTO'daki [Name] beyanlarindan kurar; GtfsReference tasiyan kolonlari kimlik donusturucusune baglar.
    private static ClassMap ColumnsOf<TDto>(CsvContext context, GtfsKeyConverter keys)
    {
        var map = context.AutoMap<TDto>();
        foreach (var key in map.MemberMaps.Where(member => member.Data.Member!.IsDefined(typeof(GtfsReferenceAttribute), false)))
            key.TypeConverter(keys);
        return map;
    }

    // Ayni kolona baglanan ikinci uyeyi (Id ve GtfsXId) yazimda bir kez tutar.
    private static ClassMap WithoutRepeatedColumns(ClassMap map)
    {
        foreach (var repeated in map.MemberMaps.GroupBy(member => member.Data.Names[0])
                     .SelectMany(column => column.Skip(1)))
            repeated.Ignore();
        return map;
    }

    // Eksik istege bagli kolonu bos, kacisiz tirnagi metin, cevrilemeyen degeri konum bilgili is hatasi sayan ayari kurar; kimlik
    // cevirisinin kendi is hatasi (tanimsiz lookup kodu) kodunu korur, konum bilgisi eklenir.
    private static CsvConfiguration CreateConfiguration(string fileName) => new(CultureInfo.InvariantCulture)
    {
        TrimOptions = TrimOptions.Trim,
        HeaderValidated = null,
        MissingFieldFound = null,
        BadDataFound = null,
        ReadingExceptionOccurred = args => throw (args.Exception.InnerException as BusinessException
                ?? new BusinessException(FeedExceptionCodes.RecordInvalid, innerException: args.Exception))
            .WithData(FeedConsts.FileDataKey, fileName)
            .WithData(FeedConsts.RowDataKey, args.Exception.Context?.Parser?.Row ?? 0)
            .WithData(FeedConsts.FieldDataKey, FieldOf(args.Exception))
    };

    // Hatali hucrenin kolon adini; okuyucu konumu yoksa donusturulemeyen uyenin GTFS kolon adini verir.
    private static string FieldOf(CsvHelperException exception)
        => exception.Context?.Reader is { HeaderRecord: { } headers, CurrentIndex: var index }
            && index >= 0 && index < headers.Length
                ? headers[index]
                : (exception as CsvHelper.TypeConversion.TypeConverterException)?.MemberMapData.Names.FirstOrDefault()
                  ?? string.Empty;
}
