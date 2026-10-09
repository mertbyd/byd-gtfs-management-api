namespace Byd.GtfsManagement.Permissions;

// islevi: Route okuma ve yonetim izinlerini tanimlar.
// sistemdeki gorevi: routes.txt endpointlerinin erisim sozlesmesini sabitler.
public static partial class GtfsManagementPermissions
{
    public static class Routes
    {
        public const string Default = GroupName + ".Routes";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
