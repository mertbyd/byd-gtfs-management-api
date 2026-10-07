using Volo.Abp.Reflection;

namespace Byd.GtfsManagement.Permissions;

public class GtfsManagementPermissions
{
    public const string GroupName = "GtfsManagement";

    public static string[] GetAll() =>
        ReflectionHelper.GetPublicConstantsRecursively(typeof(GtfsManagementPermissions));
}
