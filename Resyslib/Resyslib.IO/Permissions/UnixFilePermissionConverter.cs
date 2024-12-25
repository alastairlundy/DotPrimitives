/*
    Resyslib.IO 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

using Resyslib.Internal.Localizations;

namespace Resyslib.IO.Permissions;

public static class UnixFilePermissionConverter
{
    /// <summary>
    /// Converts a Unix file permission in symbolic notation to Octal notation.
    /// </summary>
    /// <param name="symbolicNotation">The symbolic notation to be converted to octal notation.</param>
    /// <returns>the octal notation equivalent of the specified symbolic notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid symbolic notation is specified.</exception>
    public static string ToNumericNotation(string symbolicNotation)
    {
        if (symbolicNotation.Length == 10)
        {
            return symbolicNotation switch
            {
                "----------" => "0000",
                "---x--x--x" => "0111",
                "--w--w--w-" => "0222",
                "--wx-wx-wx" => "0333",
                "-r--r--r--" => "0444",
                "-r-xr-xr-x" => "0555",
                "-rw-rw-rw-" => "0666",
                "-rwx------" => "0700",
                "-rwxr-----" => "0740",
                "-rwxrwx---" => "0770",
                "-rwxrwxrwx" => "0777",
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation)
            };
        }

        throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation);
    }

    /// <summary>
    /// Converts a Unix file permission in symbolic notation to Octal notation.
    /// </summary>
    /// <param name="numericNotation">The octal notation to be converted to symbolic notation.</param>
    /// <returns>the symbolic notation equivalent of the specified octal notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid octal notation is specified.</exception>
    public static string ToSymbolicNotation(string numericNotation)
    {
        if (numericNotation.Length == 4 && int.TryParse(numericNotation, out int result))
        {
            return result switch
            {
                0 => "----------",
                111 => "---x--x--x",
                222 => "--w--w--w-",
                333 => "--wx-wx-wx",
                444 => "-r--r--r--",
                555 => "-r-xr-xr-x",
                666 => "-rw-rw-rw-",
                700 => "-rwx------",
                740 => "-rwxr-----",
                770 => "-rwxrwx---",
                777 => "-rwxrwxrwx",
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation)
            };
        }

        throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation);
    }
}