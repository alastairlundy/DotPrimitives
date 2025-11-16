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
using System.Runtime.Versioning;

namespace AlastairLundy.DotPrimitives.IO.Paths;

/// <summary>
/// 
/// </summary>
public static class PathVariable
{
    /// <summary>
    /// 
    /// </summary>
    public static char PathContentsSeparatorChar 
        => OperatingSystem.IsWindows() ? ';' : ':';

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string[]? GetContents()
    {
        return Environment.GetEnvironmentVariable("PATH")
            ?.Split(PathContentsSeparatorChar, StringSplitOptions.RemoveEmptyEntries)
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Select(x =>
            {
                x = x.Trim();
                x = Environment.ExpandEnvironmentVariables(x);
                x = x.Trim('"');
                const string homeToken = "$HOME";
                string userProfile = Environment.GetFolderPath(
                    Environment.SpecialFolder.UserProfile
                );

                int homeTokenIndex = x.IndexOf(
                    homeToken,
                    StringComparison.CurrentCultureIgnoreCase
                );

                if (x.StartsWith('~'))
                {
                    x =
                        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{x.Substring(1)}";
                }
                if (homeTokenIndex != -1)
                {
                    return $"{x.Substring(0, homeTokenIndex)}{userProfile}{x.Substring(homeTokenIndex + homeToken.Length)}";
                }

                x = x.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                return x;
            })
            .ToArray();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string[] GetPathFileExtensions()
    {
        if (OperatingSystem.IsWindows())
        {
            return Environment
                .GetEnvironmentVariable("PATHEXT")
                ?.Split(PathContentsSeparatorChar, StringSplitOptions.RemoveEmptyEntries)
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(x =>
                {
                    x = x.Trim();
                    x = x.Trim('"');
                    if (!x.StartsWith('.'))
                        x = x.Insert(0, ".");

                    return x;
                })
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray() ?? [".COM", ".EXE", ".BAT", ".CMD"];
        }

        return [];
    }
}