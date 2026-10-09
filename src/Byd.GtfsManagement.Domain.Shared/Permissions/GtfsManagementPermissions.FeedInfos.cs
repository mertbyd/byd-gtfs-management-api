namespace Byd.GtfsManagement.Permissions;

// islevi: FeedInfo yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS yayimci bilgisi erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class FeedInfos
    {
        public const string Default = GroupName + ".FeedInfos";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
