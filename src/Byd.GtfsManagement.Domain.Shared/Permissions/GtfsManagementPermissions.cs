using Volo.Abp.Reflection;

namespace Byd.GtfsManagement.Permissions;

// islevi: Modulun permission grubunu ve tum permission adlarini bildirir.
// sistemdeki gorevi: Bounded feature izinlerinin ortak kokudur.
public static partial class GtfsManagementPermissions
{
    public const string GroupName = "GtfsManagement";

    // Tanimli izinleri ABP kesfi icin dondurur.
    public static string[] GetAll() =>
        ReflectionHelper.GetPublicConstantsRecursively(typeof(GtfsManagementPermissions));
}
