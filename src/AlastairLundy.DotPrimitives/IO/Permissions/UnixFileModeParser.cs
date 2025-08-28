/*
    MIT License
   
    Copyright (c) 2025 Alastair Lundy
   
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
   
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
   
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
 */

using System;
using System.IO;
using System.Linq;

using AlastairLundy.DotPrimitives.Internals.Localizations;

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
        if (int.TryParse(permissionNotation, out int result) == false)
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
    public static UnixFileMode ParseRwxPermissionNotation(string permissionNotation)
    {
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
}
#endif