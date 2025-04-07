/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
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

using AlastairLundy.Resyslib.IO.Localizations;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace AlastairLundy.Resyslib.IO.Permissions
{
    public static class UnixFilePermissionConverter
    {
        /// <summary>
        /// Converts a Unix file permission in symbolic notation to Octal notation.
        /// </summary>
        /// <param name="symbolicNotation">The symbolic notation to be converted to octal notation.</param>
        /// <returns>The octal notation equivalent of the specified symbolic notation.</returns>
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
                    _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation.Replace("{x}", symbolicNotation))
                };
            }

            throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation.Replace("{x}", symbolicNotation));
        }

        /// <summary>
        /// Converts a Unix file permission in octal notation to symbolic notation.
        /// </summary>
        /// <param name="numericNotation">The octal notation to be converted to symbolic notation.</param>
        /// <returns>The symbolic notation equivalent of the specified octal notation.</returns>
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
                    _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation.Replace("{x}", numericNotation))
                };
            }

            throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation.Replace("{x}", numericNotation));
        }
    }
}