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
/// Represents the set of file system permissions available on Windows.
/// </summary>
[Flags]
public enum WindowsFilePermission
{
    /// <summary>
    /// 
    /// </summary>
    SystemFullControl,
    /// <summary>
    /// 
    /// </summary>
    SystemModify,
    /// <summary>
    /// 
    /// </summary>
    SystemRead,
    /// <summary>
    /// 
    /// </summary>
    SystemWrite,
    /// <summary>
    /// 
    /// </summary>
    SystemReadAndExecute,
    /// <summary>
    /// 
    /// </summary>
    SystemListFolderContents,
    /// <summary>
    /// 
    /// </summary>
    UserFullControl,
    /// <summary>
    /// 
    /// </summary>
    UserModify,
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
    UserReadAndExecute,
    /// <summary>
    /// 
    /// </summary>
    UserListFolderContents,
    /// <summary>
    /// 
    /// </summary>
    GroupFullControl,
    /// <summary>
    /// 
    /// </summary>
    GroupModify,
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
    GroupReadAndExecute,
    /// <summary>
    /// 
    /// </summary>
    GroupListFolderContents,
}