namespace Byd.GtfsManagement.Permissions;

// islevi: Calendars okuma ve yonetim izinlerini tanimlar.
// sistemdeki gorevi: Ilgili endpointlerin erisim sozlesmesini sabitler.
public static partial class GtfsManagementPermissions
{
    public static class Calendars
    {
        public const string Default = GroupName + ".Calendars";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
