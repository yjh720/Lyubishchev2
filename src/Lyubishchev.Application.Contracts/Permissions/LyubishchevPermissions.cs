namespace Lyubishchev.Permissions;

public static class LyubishchevPermissions
{
    public const string GroupName = "Lyubishchev";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public static class TimePeriods
    {
        public const string Default = GroupName + ".TimePeriods";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
