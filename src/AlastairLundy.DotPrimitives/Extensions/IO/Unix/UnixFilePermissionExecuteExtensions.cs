/*
    AlastairLundy.DotPrimitives
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using AlastairLundy.DotPrimitives.IO.Permissions;

namespace AlastairLundy.DotPrimitives.Extensions.IO.Unix;

public static class UnixFilePermissionExecuteExtensions
{
    /// <summary>
    /// Determines whether the specified Unix file mode has execute permission.
    /// </summary>
    /// <param name="mode">The Unix file mode to check.</param>
    /// <returns>True if the mode includes execute permission, false otherwise.</returns>
    public static bool HasExecutePermission(this UnixFilePermission mode)
    {
        return mode switch
        {
            UnixFilePermission.OtherExecute or UnixFilePermission.UserExecute
                or UnixFilePermission.GroupExecute => true,
            _ => false
        };
    }
}