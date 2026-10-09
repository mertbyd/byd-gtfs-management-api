namespace Byd.GtfsManagement.Permissions;

// islevi: Agency okuma ve yonetim izinlerini tanimlar.
// sistemdeki gorevi: agency.txt endpointlerinin erisim sozlesmesini sabitler.
public static partial class GtfsManagementPermissions
{
    public static class Agencies
    {
        public const string Default = GroupName + ".Agencies";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
