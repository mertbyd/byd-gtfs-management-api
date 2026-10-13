namespace Byd.GtfsManagement.Permissions;

// islevi: FareRules yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS ucret kurali erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class FareRules
    {
        public const string Default = GroupName + ".FareRules";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
