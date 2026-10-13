namespace Byd.GtfsManagement.Permissions;

// islevi: FareLegRules yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS ucret etap kurali erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class FareLegRules
    {
        public const string Default = GroupName + ".FareLegRules";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
