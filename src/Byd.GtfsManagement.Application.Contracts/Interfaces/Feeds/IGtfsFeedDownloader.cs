using System.Threading;
using System.Threading.Tasks;

namespace Byd.GtfsManagement.Interfaces.Feeds;

// islevi: Verilen HTTP(S) GTFS kaynak adresinden guncel ZIP'in byte'larini indirir.
// sistemdeki gorevi: Kaynaktan cekme ucu ile dis HTTP entegrasyonu arasindaki sozlesmedir; yalniz indirir, indirilen ZIP'i AppService
// yukleme girdisine cevirir ve yukleme ile ayni akisa sokar.
public interface IGtfsFeedDownloader
{
    Task<byte[]> DownloadAsync(string sourceUrl, CancellationToken cancellationToken = default);
}
