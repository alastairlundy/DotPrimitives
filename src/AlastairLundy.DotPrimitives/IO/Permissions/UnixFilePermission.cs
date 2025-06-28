/*
    AlastairLundy.DotPrimitives
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

namespace AlastairLundy.DotPrimitives.IO.Permissions;

/// <summary>
/// Enum representing different types of file permissions in Unix.
/// </summary>
[Flags]
public enum UnixFilePermission
{
    /// <summary>
    /// Represents no permissions for a file in Unix.
    /// </summary>
    None,
    /// <summary>
    /// Represents the "other execute" permission in Unix,
    /// which allows the execution of a file by others.
    /// </summary>
    OtherExecute,
    /// <summary>
    /// 
    /// </summary>
    OtherWrite,
    /// <summary>
    /// 
    /// </summary>
    OtherRead,
    /// <summary>
    /// 
    /// </summary>
    GroupExecute,
    /// <summary>
    /// 
    /// </summary>
    GroupRead,
    /// <summary>
    /// 
    /// </summary>
    GroupWrite,
    /// <summary>
    /// 
    /// </summary>
    UserExecute,
    /// <summary>
    /// 
    /// </summary>
    UserRead,
    /// <summary>
    /// 
    /// </summary>
    UserWrite,

    /// <summary>
    /// 
    /// </summary>
    SetGroup,

    /// <summary>
    /// 
    /// </summary>
    SetUser,

    /// <summary>
    /// 
    /// </summary>
    StickyBit
}