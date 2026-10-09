namespace Byd.GtfsManagement.Permissions;

// islevi: BookingRules yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS rezervasyon kurali erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class BookingRules
    {
        public const string Default = GroupName + ".BookingRules";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
