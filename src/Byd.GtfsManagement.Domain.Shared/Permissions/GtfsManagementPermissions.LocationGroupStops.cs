namespace Byd.GtfsManagement.Permissions;

// islevi: LocationGroupStops yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS konum grubu durak iliskisi erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class LocationGroupStops
    {
        public const string Default = GroupName + ".LocationGroupStops";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
