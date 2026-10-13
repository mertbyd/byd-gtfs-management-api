namespace Byd.GtfsManagement.Permissions;

// islevi: Trip okuma ve yonetim izinlerini tanimlar.
// sistemdeki gorevi: trips.txt endpointlerinin erisim sozlesmesini sabitler.
public static partial class GtfsManagementPermissions
{
    public static class Trips
    {
        public const string Default = GroupName + ".Trips";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
