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
    /// Represents the user permissions for a Unix file or directory.
    /// </summary>
    public UnixFileMode UserPermissions { get; }
    
    /// <summary>
    /// Represents the group permissions for a Unix file or directory.
    /// </summary>
    public UnixFileMode GroupPermissions { get; }
    
    /// <summary>
    /// Represents other permissions for a Unix file or directory.
    /// </summary>
    public UnixFileMode OthersPermissions { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public IUnixFilePermissionNotation Parse(string input);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="notation"></param>
    /// <returns></returns>
    public bool TryParse(string input, out IUnixFilePermissionNotation? notation);
}

#endif