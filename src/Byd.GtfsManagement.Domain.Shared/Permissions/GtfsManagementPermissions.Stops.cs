namespace Byd.GtfsManagement.Permissions;

// islevi: Stop okuma ve yonetim izinlerini tanimlar.
// sistemdeki gorevi: stops.txt endpointlerinin erisim sozlesmesini sabitler.
public static partial class GtfsManagementPermissions
{
    public static class Stops
    {
        public const string Default = GroupName + ".Stops";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
