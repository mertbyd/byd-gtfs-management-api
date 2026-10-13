namespace Byd.GtfsManagement.Permissions;

// islevi: Yolculuk planlama izin adlarini tanimlar.
// sistemdeki gorevi: Controller yetkilendirmesi ve izin tanimlari ayni sabitleri kullanir.
public static partial class GtfsManagementPermissions
{
    public static class Journeys
    {
        public const string Default = GroupName + ".Journeys";
        public const string View = Default + ".View";
    }
}
