/*
    AlastairLundy.DotPrimitives
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

#if NET8_0_OR_GREATER

using System.IO;

namespace AlastairLundy.DotPrimitives.IO.Permissions.Notations;

/// <summary>
/// 
/// </summary>
public interface IUnixFilePermissionNotation
{
    /// <summary>
    /// 
    /// </summary>
    public UnixFileMode UserPermissions { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public UnixFileMode GroupPermissions { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public UnixFileMode OthersPermissions { get; }
}

#endif