namespace Byd.GtfsManagement.Permissions;

// islevi: GTFS surumlerinin goruntuleme, duzenleme ve disa aktarma izinlerini bildirir.
// sistemdeki gorevi: Surum HTTP ucunun erisim sozlesmesini Feed tanim yonetiminden ayirir.
public static partial class GtfsManagementPermissions
{
    public static class Feeds
    {
        public const string Default = GroupName + ".Feeds";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
        public const string Export = Default + ".Export";
    }
}
