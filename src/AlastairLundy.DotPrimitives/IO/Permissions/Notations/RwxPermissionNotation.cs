/*
    AlastairLundy.DotPrimitives
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

namespace AlastairLundy.DotPrimitives.IO.Permissions.Notations;

/// <summary>
/// A struct that represents file and directory Read (R), Write (w), and Execute (X) permission notation for Unix-based operating systems.
/// </summary>
public struct RwxPermissionNotation : IUnixFilePermissionNotation, IEquatable<RwxPermissionNotation>
{
    public UnixFilePermission UserPermissions { get; private set; }
    public UnixFilePermission GroupPermissions { get; private set; }
    public UnixFilePermission OthersPermissions { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    public RwxPermissionNotation(string input)
    {
        this = Parse(input);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userPermissions"></param>
    /// <param name="groupPermissions"></param>
    /// <param name="othersPermissions"></param>
    public RwxPermissionNotation(UnixFilePermission userPermissions,
        UnixFilePermission groupPermissions,
        UnixFilePermission othersPermissions)
    {
        UserPermissions = userPermissions;
        GroupPermissions = groupPermissions;
        OthersPermissions = othersPermissions;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static RwxPermissionNotation Parse(string input)
    {
        if (IsValidNotation(input) == false)
            throw new ArgumentException();
        
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="notation"></param>
    /// <returns></returns>
    public static bool TryParse(string input, out RwxPermissionNotation? notation)
    {
        try
        {
            notation = Parse(input);
            return true;
        }
        catch
        {
            notation = null;
            return false;
        }
    }

    private static bool IsValidNotation(string input)
    {
        if (input.Length != 10) 
            return false;
        
#if NET6_0_OR_GREATER
            return input switch
            {
                "----------" or
                    "---x--x--x" or
                    "--w--w--w-" or
                    "--wx-wx-wx" or
                    "-r--r--r--" or
                    "-r-xr-xr-x" or
                    "-rw-rw-rw-" or
                    "-rwx------" or
                    "-rwxr-----" or
                    "-rwxrwx---" or
                    "-rwxrwxrwx" => true,
                _ => false
            };
#else
        return input == "----------" ||
               input == "---x--x--x" ||
               input == "--w--w--w-" ||
               input == "--wx-wx-wx" ||
               input == "-r--r--r--" ||
               input == "-r-xr-xr-x" ||
               input == "-rw-rw-rw-" ||
               input == "-rwx------" ||
               input == "-rwxr-----" ||
               input == "-rwxrwx---" ||
               input == "-rwxrwxrwx";
#endif
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(RwxPermissionNotation other)
    {
        return UserPermissions == other.UserPermissions
               && GroupPermissions == other.GroupPermissions &&
               OthersPermissions == other.OthersPermissions;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        
        return obj is RwxPermissionNotation other && Equals(other);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine((int)UserPermissions, (int)GroupPermissions, (int)OthersPermissions);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(RwxPermissionNotation left, RwxPermissionNotation right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(RwxPermissionNotation left, RwxPermissionNotation right)
    {
        return left.Equals(right) == false;
    }
}