
// ReSharper disable once RedundantUsingDirective

// ReSharper disable InconsistentNaming

#if NETSTANDARD2_0 || NETSTANDARD2_1
using OperatingSystem = Polyfills.OperatingSystemPolyfill;
#else
using System.Runtime.Versioning;
#nullable enable
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

using AlastairLundy.DotPrimitives.Runtime.Enums;
using AlastairLundy.DotPrimitives.Runtime.Exceptions;
using AlastairLundy.DotPrimitives.Runtime.Internals.Localizations;

namespace AlastairLundy.DotPrimitives.Runtime;

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
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("linux")]
#endif
    private static string GetOsReleasePropertyValue(string propertyName)
    {
        if (OperatingSystem.IsLinux())
        {
            string output = string.Empty;
                
            string[] osReleaseInfo = File.ReadAllLines("/etc/os-release");
                
            foreach (string s in osReleaseInfo)
            {
                if (s.ToUpper().StartsWith(propertyName))
                {
                    output = s.Replace(propertyName, string.Empty);
                }
            }

            return output;
        }
        else
        {
            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_LinuxOnly);
        }
    }
        
    /// <summary>
    /// Returns the OS name as a string in the format that a RuntimeID uses.
    /// </summary>
    /// <param name="identifierType"></param>
    /// <returns></returns>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("macos")]
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("freebsd")]
        [SupportedOSPlatform("ios")]
        [SupportedOSPlatform("tvos")]
        [SupportedOSPlatform("watchos")]
        [SupportedOSPlatform("android")]
        [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
#endif
    internal static string GetOsNameString(RuntimeIdentifierType identifierType)
    {
#if NET5_0_OR_GREATER
            string? osName = null;
#else
        string osName = string.Empty;
#endif
            
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
                else if (identifierType == RuntimeIdentifierType.Specific)
                {
                    osName = GetOsReleasePropertyValue("IDENTIFIER_LIKE=");
                }
                else if (identifierType == RuntimeIdentifierType.DistroSpecific || identifierType == RuntimeIdentifierType.VersionLessDistroSpecific)
                {
                    osName = GetOsReleasePropertyValue("IDENTIFIER=");
                }
                else
                {
                    osName = "linux";
                }
            }
        }

        if (osName == null || string.IsNullOrEmpty(osName))
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
#if NET5_0_OR_GREATER
            string? osVersion = null;
#else
        string osVersion = string.Empty;
#endif
        if (OperatingSystem.IsWindows())
        {
            OperatingSystem operatingSystem = new OperatingSystem(PlatformID.Win32NT,
                Environment.OSVersion.Version);
                
            bool isWindows10 = OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240) &&
                               operatingSystem.Version  < new Version(10, 0, 20349);
                
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
            else if (!isWindows10 && !isWindows11)
            {
                throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_EndOfLifeOperatingSystems);
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
                    
                if (OperatingSystem.IsMacOSVersionAtLeast(11))
                {
                    osVersion = $"{version.Major}";
                }
                else
                {
                    osVersion = $"{version.Major}.{version.Major}";
                }
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        if (osVersion == null)
        {
            throw new PlatformNotSupportedException();
        }

        return osVersion;
    }

    /// <summary>
    /// Programmatically generates a .NET Runtime Identifier.
    /// Note: Microsoft advises against programmatically creating Runtime IDs but this may be necessary in some instances.
    /// For More Information Visit: https://learn.microsoft.com/en-us/dotnet/core/rid-catalog
    /// </summary>
    /// <param name="identifierType">The type of Runtime Identifier to generate.</param>
    /// <returns>the programatically generated .NET Runtime Identifier.</returns>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("macos")]
        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("freebsd")]
        [SupportedOSPlatform("ios")]
        [SupportedOSPlatform("android")]
        [SupportedOSPlatform("tvos")]
        [SupportedOSPlatform("watchos")]
        [UnsupportedOSPlatform("browser")]
#endif
    public static string GenerateRuntimeIdentifier(RuntimeIdentifierType identifierType)
    {
        if (identifierType == RuntimeIdentifierType.AnyGeneric)
        {
            return GenerateRuntimeIdentifier(identifierType, false, false);
        }

        if (identifierType == RuntimeIdentifierType.Generic ||
            identifierType == RuntimeIdentifierType.Specific && (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD()) ||
            identifierType == RuntimeIdentifierType.VersionLessDistroSpecific)
        {
            return GenerateRuntimeIdentifier(identifierType, true, false);
        }

        if (identifierType == RuntimeIdentifierType.Specific && (!OperatingSystem.IsLinux() && !OperatingSystem.IsFreeBSD())
            || identifierType == RuntimeIdentifierType.DistroSpecific)
        {
            return GenerateRuntimeIdentifier(identifierType, true, true);
        }

        return GenerateRuntimeIdentifier(RuntimeIdentifierType.Generic, true, false);
    }
        
    /// <summary>
    /// Programmatically generates a .NET Runtime Identifier based on the system calling the method.
    ///
    /// Note: Microsoft advises against programmatically creating Runtime IDs but this may be necessary in some instances.
    /// For more information visit: https://learn.microsoft.com/en-us/dotnet/core/rid-catalog
    /// </summary>
    /// <returns>the programatically generated .NET Runtime Identifier.</returns>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("macos")]
        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("freebsd")]
        [SupportedOSPlatform("ios")]
        [SupportedOSPlatform("android")]
        [SupportedOSPlatform("tvos")]
        [SupportedOSPlatform("watchos")]
        [UnsupportedOSPlatform("browser")]
#endif
    public static string GenerateRuntimeIdentifier(RuntimeIdentifierType identifierType, bool includeOperatingSystemName, bool includeOperatingSystemVersion)
    {
        string osName = GetOsNameString(identifierType);
        string cpuArch = GetArchitectureString();
            
        if (identifierType == RuntimeIdentifierType.AnyGeneric ||
            identifierType == RuntimeIdentifierType.Generic && includeOperatingSystemName == false)
        {
            return $"any-{GetArchitectureString()}";
        }

        if (identifierType == RuntimeIdentifierType.Generic && includeOperatingSystemName)
        {
            return $"{osName}-{cpuArch}";
        }

        if (identifierType == RuntimeIdentifierType.Specific ||
            OperatingSystem.IsLinux() && identifierType == RuntimeIdentifierType.DistroSpecific ||
            OperatingSystem.IsLinux() && identifierType == RuntimeIdentifierType.VersionLessDistroSpecific)
        {
            string osVersion = GetOsVersionString();

            if (OperatingSystem.IsWindows())
            {
                if (includeOperatingSystemVersion)
                {
                    return $"{osName}{osVersion}-{cpuArch}";
                }

                return $"{osName}-{cpuArch}";
            }

            if (OperatingSystem.IsMacOS())
            {
                if (includeOperatingSystemVersion)
                {
                    return $"{osName}.{osVersion}-{cpuArch}";
                }

                return $"{osName}-{cpuArch}";
            }

            if (((OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD()) && 
                    (identifierType == RuntimeIdentifierType.DistroSpecific) || identifierType == RuntimeIdentifierType.VersionLessDistroSpecific))
            {
                if (includeOperatingSystemVersion)
                {
                    return $"{osName}.{osVersion}-{cpuArch}";
                }

                return $"{osName}-{cpuArch}";
            }
            if (((OperatingSystem.IsLinux() && identifierType == RuntimeIdentifierType.Specific) && includeOperatingSystemVersion == false) ||
                includeOperatingSystemVersion == false)
            {
                return $"{osName}-{cpuArch}";
            }
        }
        else if((!OperatingSystem.IsLinux() && !OperatingSystem.IsFreeBSD()) && (identifierType == RuntimeIdentifierType.DistroSpecific || identifierType == RuntimeIdentifierType.VersionLessDistroSpecific))
        {
            return GenerateRuntimeIdentifier(RuntimeIdentifierType.Specific);
        }

        throw new RuntimeIdentifierGenerationException();
    }

    /// <summary>
    /// Detects the RuntimeID based on the system calling the method.
    /// </summary>
    /// <returns>the Runtime ID of system calling the method as a string.</returns>
    // ReSharper disable once InconsistentNaming
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("macos")]
        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("freebsd")]
        [SupportedOSPlatform("ios")]
        [SupportedOSPlatform("android")]
        [UnsupportedOSPlatform("browser")]
#endif
    public static string GetRuntimeIdentifier()
    {
        if (OperatingSystem.IsLinux())
        {
            return GenerateRuntimeIdentifier(RuntimeIdentifierType.DistroSpecific);
        }

        return GenerateRuntimeIdentifier(RuntimeIdentifierType.Specific);
    }

    /// <summary>
    /// Generates a generic Runtime Identifier, that does not make use of an operating system version, that is applicable to the system calling the method.
    /// </summary>
    /// <returns>the generic Runtime Identifier.</returns>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("macos")]
        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("freebsd")]
        [SupportedOSPlatform("ios")]
        [SupportedOSPlatform("android")]
        [UnsupportedOSPlatform("browser")]
#endif
    public static string GetGenericRuntimeIdentifier()
    {
        return GenerateRuntimeIdentifier(RuntimeIdentifierType.Generic);
    }

    /// <summary>
    /// Detects possible Runtime Identifiers that could be applicable to the system calling the method.
    /// </summary>
    /// <returns>all Runtime Identifiers that are applicable for the system calling the method.</returns>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("macos")]
        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("freebsd")]
        [SupportedOSPlatform("ios")]
        [SupportedOSPlatform("android")]
        [SupportedOSPlatform("tvos")]
        [SupportedOSPlatform("watchos")]
        [UnsupportedOSPlatform("browser")]
#endif
    public static IEnumerable<string> GetPossibleRuntimeIdentifierCandidates()
    {
        List<string> output = new List<string>();

        output.Add(GenerateRuntimeIdentifier(RuntimeIdentifierType.AnyGeneric));
        output.Add(GenerateRuntimeIdentifier(RuntimeIdentifierType.Generic));
        output.Add(GenerateRuntimeIdentifier(RuntimeIdentifierType.Specific));
            
        if (OperatingSystem.IsLinux())
        {
            output.Add(GenerateRuntimeIdentifier(RuntimeIdentifierType.VersionLessDistroSpecific));
            output.Add(GenerateRuntimeIdentifier(RuntimeIdentifierType.DistroSpecific, true, true));
        }

        return output;
    }
}