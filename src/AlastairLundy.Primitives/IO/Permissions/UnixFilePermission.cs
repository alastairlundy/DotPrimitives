/*
    AlastairLundy.Primitives
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

namespace AlastairLundy.Primitives.IO.Permissions;

/// <summary>
/// 
/// </summary>
[Flags]
public enum UnixFilePermission
{
    /// <summary>
    /// 
    /// </summary>
    None,
    /// <summary>
    /// 
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