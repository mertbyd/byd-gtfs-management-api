namespace Byd.GtfsManagement.Permissions;

// islevi: Frequencies okuma ve yonetim izinlerini tanimlar.
// sistemdeki gorevi: Endpoint erisim sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>Sefer sikligi izinlerini tanimlar.</summary>
public static partial class GtfsManagementPermissions
{
    // islevi: Frequency View ve Manage izin adlarini gruplar.
    // sistemdeki gorevi: Yetki agacina kararli bir aile sunar.
    /// <summary>Sefer sikligi izin ailesini tanimlar.</summary>
    public static class Frequencies
    {
        public const string Default = GroupName + ".Frequencies";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
