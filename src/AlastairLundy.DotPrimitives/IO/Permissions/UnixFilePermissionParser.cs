/*
    AlastairLundy.DotPrimitives
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System;
using System.Linq;
using AlastairLundy.DotExtensions.IO.Unix;
using AlastairLundy.DotPrimitives.Internal;

// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.DotPrimitives.IO.Permissions;

/// <summary>
/// A class to parse Numeric or Symbolic notation to UnixFilePermission.
/// </summary>
public static class UnixFilePermissionParser
{
    /// <summary>
    /// Parse a Unix file permission in octal notation to a UnixFilePermission enum.
    /// </summary>
    /// <param name="permissionNotation">The octal notation to be parsed.</param>
    /// <returns>The UnixFilePermission enum equivalent to the specified octal notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid octal notation is specified.</exception>
    public static UnixFilePermission ParseNumericNotation(string permissionNotation)
    {
            if (permissionNotation.IsNumericNotation() == false
            || int.TryParse(permissionNotation, out int result) == false)
            throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation);
        
            if(permissionNotation.StartsWith("0"))
                permissionNotation =  permissionNotation.Remove(0, 1);
        
            int user = int.Parse(permissionNotation.First().ToString());
            int group = int.Parse(permissionNotation[^2].ToString());
            int others = int.Parse(permissionNotation.Last().ToString());

            UnixFilePermission output = user switch
            {
                0 => UnixFilePermission.None,
                1 => UnixFilePermission.UserExecute,
                2 => UnixFilePermission.UserWrite,
                3 => UnixFilePermission.UserWrite & UnixFilePermission.UserExecute,
                4 => UnixFilePermission.UserRead,
                5 => UnixFilePermission.UserRead & UnixFilePermission.UserExecute,
                6 => UnixFilePermission.UserRead & UnixFilePermission.UserWrite,
                7 => UnixFilePermission.UserRead & UnixFilePermission.UserWrite & UnixFilePermission.UserExecute,
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
            };
         
            output = group switch
            {
                0 => output & UnixFilePermission.None,
                1 => output & UnixFilePermission.GroupExecute,
                2 => output & UnixFilePermission.GroupWrite,
                3 => output & UnixFilePermission.GroupWrite & UnixFilePermission.GroupExecute,
                4 => output & UnixFilePermission.GroupRead,
                5 => output & UnixFilePermission.GroupRead & UnixFilePermission.GroupExecute,
                6 => output & UnixFilePermission.GroupRead & UnixFilePermission.GroupWrite,
                7 => output & UnixFilePermission.GroupRead & UnixFilePermission.GroupWrite & UnixFilePermission.GroupExecute,
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
            };
        
            output = others switch
            {
                0 => output & UnixFilePermission.None,
                1 => output & UnixFilePermission.OtherExecute,
                2 => output & UnixFilePermission.OtherWrite,
                3 => output & UnixFilePermission.OtherWrite & UnixFilePermission.OtherExecute,
                4 => output & UnixFilePermission.OtherRead,
                5 => output & UnixFilePermission.OtherRead & UnixFilePermission.OtherExecute, 6 => output & UnixFilePermission.OtherRead & UnixFilePermission.OtherWrite,
                7 => output & UnixFilePermission.OtherRead & UnixFilePermission.OtherWrite & UnixFilePermission.OtherExecute,
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
            };
        
            return output;
    }

    /// <summary>
    /// Attempts to parse a Unix file permission in Octal notation to a UnixFilePermission enum. 
    /// </summary>
    /// <param name="permissionNotation">The Unix file permission octal notation to be parsed.</param>
    /// <param name="fileMode">The UnixFilePermission equivalent value to the octal notation if a valid octal notation was specified; null otherwise.</param>
    /// <returns>True if a valid Unix file permission octal notation was specified; false otherwise.</returns>
    public static bool TryParseNumericNotation(string permissionNotation, out UnixFilePermission? fileMode)
    {
        try
        {
            fileMode = ParseNumericNotation(permissionNotation);
            return true;
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }

    /// <summary>
    /// Parse a Unix file permission in symbolic notation to a UnixFilePermission enum.
    /// </summary>
    /// <param name="permissionNotation">The symbolic notation to be compared.</param>
    /// <returns>The UnixFilePermission enum equivalent to the specified symbolic notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid symbolic notation is specified.</exception>
    public static UnixFilePermission ParseSymbolicNotation(string permissionNotation)
    {
        if (permissionNotation.IsSymbolicNotation() == false)
            throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidSymbolicNotation);
        
        return permissionNotation.ToLower() switch
        {
            "----------" => UnixFilePermission.None,
            "---x--x--x" => UnixFilePermission.UserExecute,
            "--w--w--w-" => UnixFilePermission.UserWrite,
            "--wx-wx-wx" => UnixFilePermission.UserWrite & UnixFilePermission.UserExecute,
            "-r--r--r--" => UnixFilePermission.UserRead,
            "-r-xr-xr-x" => UnixFilePermission.UserRead & UnixFilePermission.UserExecute,
            "-rw-rw-rw-" => UnixFilePermission.UserRead & UnixFilePermission.UserWrite,
            "-rwx------" => UnixFilePermission.UserRead & UnixFilePermission.UserWrite &
                            UnixFilePermission.UserExecute,
            "-rwxr-----" => UnixFilePermission.UserExecute & UnixFilePermission.UserWrite &
                            UnixFilePermission.UserRead &
                            UnixFilePermission.GroupRead,
            "-rwxrwx---" => UnixFilePermission.UserRead & UnixFilePermission.UserWrite &
                            UnixFilePermission.UserExecute &
                            UnixFilePermission.GroupRead & UnixFilePermission.GroupWrite &
                            UnixFilePermission.GroupExecute,
            "-rwxrwxrwx" => UnixFilePermission.UserRead & UnixFilePermission.UserWrite &
                            UnixFilePermission.UserExecute &
                            UnixFilePermission.GroupRead & UnixFilePermission.GroupWrite &
                            UnixFilePermission.GroupExecute &
                            UnixFilePermission.OtherRead & UnixFilePermission.OtherWrite &
                            UnixFilePermission.OtherExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidSymbolicNotation)
        };

    }

    /// <summary>
    /// Attempts to parse a Unix file permission in Symbolic notation to a UnixFilePermission enum. 
    /// </summary>
    /// <param name="permissionNotation">The Unix file permission symbolic notation to be parsed.</param>
    /// <param name="fileMode">The UnixFilePermission equivalent value to the symbolic notation if a valid symbolic notation was specified; null otherwise.</param>
    /// <returns>True if a valid Unix file permission symbolic notation was specified; false otherwise.</returns>
    public static bool TryParseSymbolicNotation(string permissionNotation, out UnixFilePermission? fileMode)
    {
        try
        {
            fileMode = ParseSymbolicNotation(permissionNotation);
            return true;
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }

    /// <summary>
    /// Attempts to parse a Unix file permission in either Numeric or Symbolic notation to a UnixFilePermission enum. 
    /// </summary>
    /// <param name="permissionNotation">The Unix file permission symbolic notation to be parsed.</param>
    /// <param name="filePermission">The unix file mode if a valid permission notation was parsed; null otherwise.</param>
    /// <returns>True if the file mode notation was parsed successfully; false otherwise.</returns>
    public static bool TryParse(string permissionNotation, out UnixFilePermission? filePermission)
    {
        bool isNumericNotation = permissionNotation.IsNumericNotation();
        bool isSymbolicNotation = permissionNotation.IsSymbolicNotation();

        try
        {
            if (isNumericNotation && !isSymbolicNotation)
            {
                filePermission = ParseNumericNotation(permissionNotation);
            }
            else if (isSymbolicNotation && !isNumericNotation)
            {
                filePermission = ParseSymbolicNotation(permissionNotation);
            }
            else
            {
                filePermission = null;
                return false;
            }

            return true;
        }
        catch
        {
            filePermission = null;
            return false;
        }
    }    
}