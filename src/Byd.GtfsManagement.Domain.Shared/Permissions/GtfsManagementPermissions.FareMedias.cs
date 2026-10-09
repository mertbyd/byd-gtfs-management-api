namespace Byd.GtfsManagement.Permissions;

// islevi: FareMedias yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS ucret medyasi erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class FareMedias
    {
        public const string Default = GroupName + ".FareMedias";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
