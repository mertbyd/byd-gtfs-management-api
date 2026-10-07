using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Byd.GtfsManagement.Permissions;

public class GtfsManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GtfsManagementPermissions.GroupName, L("Permission:GtfsManagement"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GtfsManagementResource>(name);
    }
}
