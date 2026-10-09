using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement;

// islevi: Module schema ve ortak veritabani baglantisini tanimlar.
// sistemdeki gorevi: Tasarim ve runtime DbContext'lerinin ayni Default baglantisini kullanmasini saglar.
public static class GtfsManagementDbProperties
{
    public static string DbTablePrefix { get; set; } = "GtfsManagement";

    public static string? DbSchema { get; set; } = DatabaseConsts.ModuleSchema;

    public const string ConnectionStringName = "Default";
}
