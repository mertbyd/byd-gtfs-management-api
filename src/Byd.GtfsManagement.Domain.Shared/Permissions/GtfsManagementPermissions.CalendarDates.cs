namespace Byd.GtfsManagement.Permissions;

// islevi: CalendarDates okuma ve yonetim izinlerini tanimlar.
// sistemdeki gorevi: Ilgili endpointlerin erisim sozlesmesini sabitler.
public static partial class GtfsManagementPermissions
{
    public static class CalendarDates
    {
        public const string Default = GroupName + ".CalendarDates";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
