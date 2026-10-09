namespace Byd.GtfsManagement.Permissions;

// islevi: FareLegJoinRules yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS ucret etap birlestirme kurali erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class FareLegJoinRules
    {
        public const string Default = GroupName + ".FareLegJoinRules";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
