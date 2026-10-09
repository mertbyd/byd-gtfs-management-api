namespace Byd.GtfsManagement.Permissions;

// islevi: LocationGroups yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS konum grubu erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class LocationGroups
    {
        public const string Default = GroupName + ".LocationGroups";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
