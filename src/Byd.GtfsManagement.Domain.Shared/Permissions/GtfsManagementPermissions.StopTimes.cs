namespace Byd.GtfsManagement.Permissions;

// islevi: StopTimes okuma ve yonetim izinlerini tanimlar.
// sistemdeki gorevi: Ilgili endpointlerin erisim sozlesmesini sabitler.
/// <summary>StopTime izin adlarini tanimlar.</summary>
public static partial class GtfsManagementPermissions
{
    // islevi: StopTime icin View ve Manage izin adlarini gruplar.
    // sistemdeki gorevi: Endpoint yetkilerini Domain.Shared sabitlerine baglar.
    /// <summary>StopTime izin ailesini tanimlar.</summary>
    public static class StopTimes
    {
        public const string Default = GroupName + ".StopTimes";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
