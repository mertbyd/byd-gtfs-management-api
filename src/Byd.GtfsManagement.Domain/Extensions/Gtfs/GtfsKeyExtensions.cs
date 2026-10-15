using System;
using System.Security.Cryptography;
using System.Text;
using Byd.GtfsManagement.Feeds;

namespace Byd.GtfsManagement.Extensions.Gtfs;

// islevi: GTFS anahtarindan Feed'e ozgu kayit kimligini uretir.
// sistemdeki gorevi: Kimlik Feed, hedef tablo ve anahtar degerinden turetilir; ice aktarimda kaydin kendi kimligi ve ona
// yapilan her baglanti ayni degeri uretir, sozluk tutmadan cozulur.
public static class GtfsKeyExtensions
{
    // Feed, hedef tablo ve anahtar degerinden ayni Feed icinde hep ayni kalan kimligi uretir.
    public static Guid ToGuid(this GtfsKey key, Guid feedId)
        => new(SHA256.HashData(Encoding.UTF8.GetBytes($"{feedId:N}/{key.Target}/{key.Value}")).AsSpan(0, 16));
}
