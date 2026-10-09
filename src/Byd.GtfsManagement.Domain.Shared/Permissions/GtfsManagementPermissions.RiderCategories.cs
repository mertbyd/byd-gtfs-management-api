namespace Byd.GtfsManagement.Permissions;

// islevi: RiderCategories yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS yolcu kategorisi erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class RiderCategories
    {
        public const string Default = GroupName + ".RiderCategories";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
