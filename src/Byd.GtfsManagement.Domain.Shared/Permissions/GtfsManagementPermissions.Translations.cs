namespace Byd.GtfsManagement.Permissions;

// islevi: Translations yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS ceviri erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class Translations
    {
        public const string Default = GroupName + ".Translations";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
