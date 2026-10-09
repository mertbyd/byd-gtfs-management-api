namespace Byd.GtfsManagement.Permissions;

// islevi: FareAttributes yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS ucret ozelligi erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class FareAttributes
    {
        public const string Default = GroupName + ".FareAttributes";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
