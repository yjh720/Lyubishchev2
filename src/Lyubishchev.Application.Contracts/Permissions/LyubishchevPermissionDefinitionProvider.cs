using Lyubishchev.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Lyubishchev.Permissions;

public class LyubishchevPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var lyubishchevGroup = context.AddGroup(LyubishchevPermissions.GroupName, L("Permission:Lyubishchev"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(LyubishchevPermissions.MyPermission1, L("Permission:MyPermission1"));
        var timePeriodsPermission = lyubishchevGroup.AddPermission(LyubishchevPermissions.TimePeriods.Default, L("Permission:TimePeriods"));
        timePeriodsPermission.AddChild(LyubishchevPermissions.TimePeriods.Create, L("Permission:TimePeriods.Create"));
        timePeriodsPermission.AddChild(LyubishchevPermissions.TimePeriods.Edit, L("Permission:TimePeriods.Edit"));
        timePeriodsPermission.AddChild(LyubishchevPermissions.TimePeriods.Delete, L("Permission:TimePeriods.Delete"));

        var timePeriodCategoryPermission = lyubishchevGroup.AddPermission(
            LyubishchevPermissions.TimePeriodCategories.Default, L("Permission:TimePeriodCategories"));
        timePeriodCategoryPermission.AddChild(
            LyubishchevPermissions.TimePeriodCategories.Create, L("Permission:TimePeriodCategories.Create"));
        timePeriodCategoryPermission.AddChild(
            LyubishchevPermissions.TimePeriodCategories.Edit, L("Permission:TimePeriodCategories.Edit"));
        timePeriodCategoryPermission.AddChild(
            LyubishchevPermissions.TimePeriodCategories.Delete, L("Permission:TimePeriodCategories.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LyubishchevResource>(name);
    }
}
