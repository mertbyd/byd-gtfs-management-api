namespace Byd.GtfsManagement.Permissions;

// islevi: Services okuma ve yonetim izinlerini tanimlar.
// sistemdeki gorevi: Ilgili endpointlerin erisim sozlesmesini sabitler.
public static partial class GtfsManagementPermissions
{
    public static class Services
    {
        public const string Default = GroupName + ".Services";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
