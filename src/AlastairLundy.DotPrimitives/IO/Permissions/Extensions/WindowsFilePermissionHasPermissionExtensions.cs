/*
      AlastairLundy.DotPrimitives
      Copyright (c) 2024-2025 Alastair Lundy

      This Source Code Form is subject to the terms of the Mozilla Public
      License, v. 2.0. If a copy of the MPL was not distributed with this
      file, You can obtain one at http://mozilla.org/MPL/2.0/.
   */

namespace AlastairLundy.DotPrimitives.IO.Permissions;

/// <summary>
/// 
/// </summary>
public static class WindowsFilePermissionHasPermissionExtensions
{
  
    /// <summary>
    /// Determines whether the specified Windows file permission has execute permission.
    /// </summary>
    /// <param name="permission">The Windows file permission to check.</param>
    /// <returns>True if the permission includes execute permission, false otherwise.</returns>
    public static bool HasExecutePermission(this WindowsFilePermission permission)
    {
        return permission.HasFlag(WindowsFilePermission.GroupReadAndExecute) ||
               permission.HasFlag(WindowsFilePermission.SystemReadAndExecute) ||
               permission.HasFlag(WindowsFilePermission.UserReadAndExecute) ||
               permission.HasFlag(WindowsFilePermission.GroupFullControl) ||
               permission.HasFlag(WindowsFilePermission.UserFullControl) ||
               permission.HasFlag(WindowsFilePermission.SystemFullControl);
    }

    /// <summary>
    /// Determines whether the specified Windows file permission has execute permission.
    /// </summary>
    /// <param name="permission">The Windows file permission to check.</param>
    /// <returns>True if the permission includes execute permission, false otherwise.</returns>
    public static bool HasWritePermission(this WindowsFilePermission permission)
    {
        return permission.HasFlag(WindowsFilePermission.GroupWrite) ||
               permission.HasFlag(WindowsFilePermission.SystemWrite) ||
               permission.HasFlag(WindowsFilePermission.UserWrite) ||
               permission.HasFlag(WindowsFilePermission.GroupFullControl) ||
               permission.HasFlag(WindowsFilePermission.UserFullControl) ||
               permission.HasFlag(WindowsFilePermission.SystemFullControl);
    }

    /// <summary>
    /// Determines whether the specified Windows file permission has execute permission.
    /// </summary>
    /// <param name="permission">The Windows file permission to check.</param>
    /// <returns>True if the permission includes execute permission, false otherwise.</returns>
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