using AlastairLundy.DotPrimitives.IO.Permissions;

namespace AlastairLundy.DotPrimitives.Extensions.IO.Windows;

internal static class WindowsFilePermissionHasPermissionExtensions
{
    
  
    /// <summary>
    /// 
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    public static bool HasExecutePermission(this WindowsFilePermission permission)
    {
        return permission.HasFlag(WindowsFilePermission.GroupReadAndExecute) ||
               permission.HasFlag(WindowsFilePermission.SystemReadAndExecute) ||
               permission.HasFlag(WindowsFilePermission.UserReadAndExecute) ||
               permission.HasFlag(WindowsFilePermission.GroupFullControl) ||
               permission.HasFlag(WindowsFilePermission.UserFullControl) ||
               permission.HasFlag(WindowsFilePermission.SystemFullControl);
    }

    public static bool HasWritePermission(this WindowsFilePermission permission)
    {
        return permission.HasFlag(WindowsFilePermission.GroupWrite) ||
               permission.HasFlag(WindowsFilePermission.SystemWrite) ||
               permission.HasFlag(WindowsFilePermission.UserWrite) ||
               permission.HasFlag(WindowsFilePermission.GroupFullControl) ||
               permission.HasFlag(WindowsFilePermission.UserFullControl) ||
               permission.HasFlag(WindowsFilePermission.SystemFullControl);
    }

    public static bool HasReadPermission(this WindowsFilePermission permission)
    {
        return permission.HasFlag(WindowsFilePermission.GroupRead) ||
               permission.HasFlag(WindowsFilePermission.SystemRead) ||
               permission.HasFlag(WindowsFilePermission.UserRead) ||
               permission.HasFlag(WindowsFilePermission.GroupReadAndExecute) ||
               permission.HasFlag(WindowsFilePermission.SystemReadAndExecute) ||
               permission.HasFlag(WindowsFilePermission.UserReadAndExecute) ||
               permission.HasFlag(WindowsFilePermission.GroupFullControl) ||
               permission.HasFlag(WindowsFilePermission.UserFullControl) ||
               permission.HasFlag(WindowsFilePermission.SystemFullControl);
    }
  
}