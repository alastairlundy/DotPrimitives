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