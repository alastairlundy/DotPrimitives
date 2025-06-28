/*
    AlastairLundy.DotPrimitives
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.IO;
using System.Linq;
using AlastairLundy.DotExtensions.IO.Unix;
using AlastairLundy.DotPrimitives.Internal;

namespace AlastairLundy.DotPrimitives.IO.Permissions;

#if NET8_0_OR_GREATER 

/// <summary>
/// A class to parse Numeric or Symbolic notation to UnixFileMode.
/// </summary>
public static class UnixFileModeParser
{
    /// <summary>
    /// Parse a Unix file permission in octal notation to a UnixFileMode enum.
    /// </summary>
    /// <param name="permissionNotation">The octal notation to be parsed.</param>
    /// <returns>The UnixFileMode enum equivalent to the specified octal notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid octal notation is specified.</exception>
    public static UnixFileMode ParseNumericNotation(string permissionNotation)
    {
        if (permissionNotation.IsNumericNotation() == false
            || int.TryParse(permissionNotation, out int result) == false)
            throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation);
        
        if(permissionNotation.StartsWith("0"))
            permissionNotation =  permissionNotation.Remove(0, 1);
        
        int user = int.Parse(permissionNotation.First().ToString());
        int group = int.Parse(permissionNotation[^2].ToString());
        int others = int.Parse(permissionNotation.Last().ToString());

        UnixFileMode output = user switch
        {
            0 => UnixFileMode.None,
            1 => UnixFileMode.UserExecute,
            2 => UnixFileMode.UserWrite,
            3 => UnixFileMode.UserWrite & UnixFileMode.UserExecute,
            4 => UnixFileMode.UserRead,
            5 => UnixFileMode.UserRead & UnixFileMode.UserExecute,
            6 => UnixFileMode.UserRead & UnixFileMode.UserWrite,
            7 => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
        };
         
        output = group switch
        {
            0 => output & UnixFileMode.None,
            1 => output & UnixFileMode.GroupExecute,
            2 => output & UnixFileMode.GroupWrite,
            3 => output & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute,
            4 => output & UnixFileMode.GroupRead,
            5 => output & UnixFileMode.GroupRead & UnixFileMode.GroupExecute,
            6 => output & UnixFileMode.GroupRead & UnixFileMode.GroupWrite,
            7 => output & UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
        };
        
        output = others switch
        {
            0 => output & UnixFileMode.None,
            1 => output & UnixFileMode.OtherExecute,
            2 => output & UnixFileMode.OtherWrite,
            3 => output & UnixFileMode.OtherWrite & UnixFileMode.OtherExecute,
            4 => output & UnixFileMode.OtherRead,
            5 => output & UnixFileMode.OtherRead & UnixFileMode.OtherExecute,
            6 => output & UnixFileMode.OtherRead & UnixFileMode.OtherWrite,
            7 => output & UnixFileMode.OtherRead & UnixFileMode.OtherWrite & UnixFileMode.OtherExecute,
            _ => throw new  ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
        };
        
        return output;
    }
    
        /// <summary>
    /// Parse a Unix file permission in symbolic notation to a UnixFileMode enum.
    /// </summary>
    /// <param name="permissionNotation">The symbolic notation to be compared.</param>
    /// <returns>The UnixFileMode enum equivalent to the specified symbolic notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid symbolic notation is specified.</exception>
    public static UnixFileMode ParseSymbolicNotation(string permissionNotation)
    {
        if (permissionNotation.IsSymbolicNotation() == false)
            throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidSymbolicNotation);
        
        return permissionNotation.ToLower() switch
        {
            "----------" => UnixFileMode.None,
            "---x--x--x" => UnixFileMode.UserExecute,
            "--w--w--w-" => UnixFileMode.UserWrite,
            "--wx-wx-wx" => UnixFileMode.UserWrite & UnixFileMode.UserExecute,
            "-r--r--r--" => UnixFileMode.UserRead,
            "-r-xr-xr-x" => UnixFileMode.UserRead & UnixFileMode.UserExecute,
            "-rw-rw-rw-" => UnixFileMode.UserRead & UnixFileMode.UserWrite,
            "-rwx------" => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute,
            "-rwxr-----" => UnixFileMode.UserExecute & UnixFileMode.UserWrite & UnixFileMode.UserRead &
                            UnixFileMode.GroupRead,
            "-rwxrwx---" => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                            UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute,
            "-rwxrwxrwx" => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                            UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute &
                            UnixFileMode.OtherRead & UnixFileMode.OtherWrite & UnixFileMode.OtherExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidSymbolicNotation)
        };

    }

    /// <summary>
    /// Attempts to parse a Unix file permission in Symbolic notation to a UnixFileMode enum. 
    /// </summary>
    /// <param name="permissionNotation">The Unix file permission symbolic notation to be parsed.</param>
    /// <param name="fileMode">The UnixFileMode equivalent value to the symbolic notation if a valid symbolic notation was specified; null otherwise.</param>
    /// <returns>True if a valid Unix file permission symbolic notation was specified; false otherwise.</returns>
    public static bool TryParseSymbolicNotation(string permissionNotation, out UnixFileMode? fileMode)
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
    /// Attempts to parse a Unix file permission in either Numeric or Symbolic notation to a UnixFileMode enum. 
    /// </summary>
    /// <param name="permissionNotation">The Unix file permission symbolic notation to be parsed.</param>
    /// <param name="fileMode">The unix file mode if a valid permission notation was parsed; null otherwise.</param>
    /// <returns>True if the file mode notation was parsed successfully; false otherwise.</returns>
    public static bool TryParse(string permissionNotation, out UnixFileMode? fileMode)
    {
        bool isNumericNotation = permissionNotation.IsNumericNotation();
        bool isSymbolicNotation = permissionNotation.IsSymbolicNotation();

        try
        {
            if (isNumericNotation && isSymbolicNotation == false)
            {
                fileMode = ParseNumericNotation(permissionNotation);
                return true;
            }
            else if (isSymbolicNotation && isNumericNotation == false)
            {
                fileMode = ParseSymbolicNotation(permissionNotation);
                return true;
            }
            else
            {
                fileMode = null;
                return false;
            }
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }
}
#endif