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

// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using DotPrimitives.Meta.Internals.Localizations;

namespace DotPrimitives.Meta.Runtime;

/// <summary>
/// A class to manage RuntimeId detection and programmatic generation.
/// </summary>
public static class RuntimeIdentification
{
    /// <summary>
    /// Returns the CPU architecture as a string in the format that a RuntimeID uses.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException">Thrown if run on an unsupported platform</exception>
    private static string GetArchitectureString()
    {
        switch (RuntimeInformation.OSArchitecture)
        {
            case Architecture.Arm:
                return "arm";
            case Architecture.Arm64:
                return "arm64";
            case Architecture.X64:
                return "x64";
            case Architecture.X86:
                return "x86";
#if NET8_0_OR_GREATER
            case Architecture.Wasm:
                return "wasm";
            case Architecture.S390x:
                return "s390x";
            case Architecture.Armv6:
                return "armv6";
            case Architecture.Ppc64le:
                return "pc64le";
#endif
            default:
                throw new PlatformNotSupportedException();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
        [SupportedOSPlatform("linux")]
    private static string? GetOsReleasePropertyValue(string propertyName)
    {
        if (OperatingSystem.IsLinux() == false)
        {
            throw new PlatformNotSupportedException(
                Resources.Exceptions_PlatformNotSupported_RequiresOs
                    .Replace("{targetOs}", "Linux)")
                    .Replace("{actualOs}", RuntimeInformation.OSDescription));
        }
        
        string[] osReleaseInfo = File.ReadAllLines("/etc/os-release");

        string? output = osReleaseInfo.FirstOrDefault(x => x.StartsWith(propertyName.ToUpper()));

        return output is not null ? output.Replace(propertyName, string.Empty) : output;
    }
        
    /// <summary>
    /// Returns the OS name as a string in the format that a RuntimeID uses.
    /// </summary>
    /// <param name="identifierType"></param>
    /// <returns></returns>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("ios")]
    [SupportedOSPlatform("tvos")]
    [SupportedOSPlatform("watchos")]
    [SupportedOSPlatform("android")]
    internal static string GetOsNameString(RuntimeIdentifierType identifierType)
    {
        string? osName = null;
            
        if (identifierType == RuntimeIdentifierType.AnyGeneric)
        {
            osName = "any";
        }
        else
        {
            if (OperatingSystem.IsWindows())
            {
                osName = "win";
            }
            if (OperatingSystem.IsMacOS())
            {
                osName = "osx";
            }
            if (OperatingSystem.IsFreeBSD())
            {
                osName = "freebsd";
            }
            if (OperatingSystem.IsAndroid())
            {
                osName = "android";
            }
            if (OperatingSystem.IsIOS())
            {
                osName = "ios";
            }
            if (OperatingSystem.IsTvOS())
            {
                osName = "tvos";
            }
            if (OperatingSystem.IsWatchOS())
            {
                osName = "watchos";
            }
            if (OperatingSystem.IsLinux())
            {
                if (identifierType == RuntimeIdentifierType.Generic)
                {
                    osName = "linux";
                }
                else if (identifierType == RuntimeIdentifierType.OsSpecific)
                {
                    osName = GetOsReleasePropertyValue("IDENTIFIER_LIKE=") ?? "linux";
                }
                else if (identifierType == RuntimeIdentifierType.FullySpecific)
                {
                    osName = GetOsReleasePropertyValue("IDENTIFIER=") ?? "linux";
                }
                else
                {
                    osName = "linux";
                }
            }
        }

        if (string.IsNullOrEmpty(osName) || osName is null) 
        {
            throw new PlatformNotSupportedException();
        }

        return osName;
    }
        
    /// <summary>
    /// Returns the OS version as a string in the format that a RuntimeID uses.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    internal static string GetOsVersionString()
    {
        string? osVersion = null;

        if (OperatingSystem.IsWindows())
        {
#if NET5_0_OR_GREATER
            OperatingSystem operatingSystem = new OperatingSystem(PlatformID.Win32NT,
                Environment.OSVersion.Version);
#endif
            
            bool isWindows10 = OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240) &&
#if NET5_0_OR_GREATER
                               operatingSystem.Version  < new Version(10, 0, 20349);
#else
            Environment.OSVersion.Version < new Version(10, 0, 20349);
#endif
                
            bool isWindows11 = OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22000);
                
            if (isWindows10)
            {
                osVersion = "10";
            }
            else if (isWindows11)
            {
                osVersion = "11";
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            else if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240) == false)
            {
                throw new PlatformNotSupportedException(Resources.
                    Exceptions_PlatformNotSupported_EndOfLifeOperatingSystem);
            }
        }
        if (OperatingSystem.IsLinux())
        {
            osVersion = Environment.OSVersion.Version.ToString();
        }
        if (OperatingSystem.IsFreeBSD())
        {
            osVersion = Environment.OSVersion.Version.ToString();
                
            switch (osVersion.Count(x => x == '.'))
            {
                case 3:
                    osVersion = osVersion.Remove(osVersion.Length - 4, 4);
                    break;
                case 2:
                    osVersion = osVersion.Remove(osVersion.Length - 2, 2);
                    break;
                case 1:
                    break;
                case 0:
                    osVersion = $"{osVersion}.0";
                    break;
            }
        }
        if (OperatingSystem.IsMacOS())
        {
            bool isAtLeastHighSierra = OperatingSystem.IsMacOSVersionAtLeast(10, 13);

            Version version = Environment.OSVersion.Version;

            if (isAtLeastHighSierra)
            {
                osVersion = OperatingSystem.IsMacOSVersionAtLeast(11) 
                    ? $"{version.Major}" : $"{version.Major}.{version.Major}";
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        if (osVersion == null)
            throw new PlatformNotSupportedException();

        return osVersion;
    }
    
    /// <summary>
    /// Programmatically generates a .NET Runtime Identifier based on the system calling the method.
    ///
    /// Note: Microsoft advises against programmatically creating Runtime Identifiers, but this may be necessary in some instances.
    ///
    /// <para/>
    /// <para>For more information visit: https://learn.microsoft.com/en-us/dotnet/core/rid-catalog</para>
    /// </summary>
    /// <param name="identifierType">The type of Runtime Identifier to generate.</param>
    /// <returns>the programatically generated .NET Runtime Identifier.</returns>
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("ios")]
    [SupportedOSPlatform("android")]
    [SupportedOSPlatform("tvos")]
    [SupportedOSPlatform("watchos")]
    [UnsupportedOSPlatform("browser")]
    public static string GenerateRuntimeIdentifier(RuntimeIdentifierType identifierType)
    {
        string osName = GetOsNameString(identifierType);
        string cpuArch = GetArchitectureString();
            
        if (identifierType == RuntimeIdentifierType.AnyGeneric)
        {
            return $"any-{GetArchitectureString()}";
        }

        if (identifierType == RuntimeIdentifierType.Generic)
        {
            return $"{osName}-{cpuArch}";
        }

        if (identifierType == RuntimeIdentifierType.OsSpecific ||
            identifierType == RuntimeIdentifierType.FullySpecific)
        {
            string osVersion = GetOsVersionString();

            if (OperatingSystem.IsWindows())
            {
                if (identifierType == RuntimeIdentifierType.FullySpecific)
                {
                    return $"{osName}{osVersion}-{cpuArch}";
                }
            }

            if (OperatingSystem.IsMacOS())
            {
                if (identifierType == RuntimeIdentifierType.FullySpecific)
                {
                    return $"{osName}.{osVersion}-{cpuArch}";
                }
            }

            if (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD() || OperatingSystem.IsAndroid())
            {
                if (identifierType == RuntimeIdentifierType.FullySpecific)
                {
                    return $"{osName}.{osVersion}-{cpuArch}";
                }
            }
            
            return $"{osName}-{cpuArch}";
        }

        throw new ArgumentException("Could not find a valid operating system and Runtime Identifier Type.");
    }

    /// <summary>
    /// Detects the RuntimeID based on the system calling the method.
    /// </summary>
    /// <returns>The Runtime ID of the system calling the method as a string.</returns>
    // ReSharper disable once InconsistentNaming
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("ios")]
    [SupportedOSPlatform("android")]
    [UnsupportedOSPlatform("browser")]
    public static string GetRuntimeIdentifier()
    {
        return GenerateRuntimeIdentifier(RuntimeIdentifierType.OsSpecific);
    }

    /// <summary>
    /// Generates a generic Runtime Identifier that does not make use of an operating system version that is applicable to the system calling the method.
    /// </summary>
    /// <returns>the generic Runtime Identifier.</returns>
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("ios")]
    [SupportedOSPlatform("android")]
    [UnsupportedOSPlatform("browser")]
    public static string GetGenericRuntimeIdentifier() =>
        GenerateRuntimeIdentifier(RuntimeIdentifierType.Generic);

    /// <summary>
    /// Detects possible Runtime Identifiers that could be applicable to the system calling the method.
    /// </summary>
    /// <returns>all Runtime Identifiers that are applicable for the system calling the method.</returns>
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("ios")]
    [SupportedOSPlatform("android")]
    [SupportedOSPlatform("tvos")]
    [SupportedOSPlatform("watchos")]
    [UnsupportedOSPlatform("browser")]
    public static IEnumerable<string> GetPossibleRuntimeIdentifierCandidates()
    {
        return 
        [
            GenerateRuntimeIdentifier(RuntimeIdentifierType.AnyGeneric),
            GenerateRuntimeIdentifier(RuntimeIdentifierType.Generic),
            GenerateRuntimeIdentifier(RuntimeIdentifierType.OsSpecific),
            GenerateRuntimeIdentifier(RuntimeIdentifierType.FullySpecific)
        ];
    }
}