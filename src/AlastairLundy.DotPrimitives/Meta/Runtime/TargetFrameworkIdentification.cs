// ReSharper disable MemberCanBePrivate.Global

using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using AlastairLundy.DotPrimitives.Internals.Localizations;

#if NETSTANDARD2_0
using OperatingSystem = Polyfills.OperatingSystemPolyfill;
#endif

namespace AlastairLundy.DotPrimitives.Meta.Runtime;

/// <summary>
/// A class to manage Target Framework detection
/// </summary>
public static class TargetFrameworkIdentification
{
    /// <summary>
    /// Generates a .NET (5+) generic TFM.
    /// </summary>  
    /// <returns>the .NET (5+) generic TFM.</returns>
    // ReSharper disable once InconsistentNaming
    private static string GetNetTFM()
    {
        Version frameworkVersion = GetFrameworkVersion();
        StringBuilder stringBuilder = new StringBuilder();
        
        stringBuilder.Append(Resources.MonikerTypes_Net5Plus.Remove(0, 1));
        stringBuilder.Append(frameworkVersion.Major);
        stringBuilder.Append('.');
        stringBuilder.Append(frameworkVersion.Minor);
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Generates the .NET (5+/Core 3.1) operating system specificTFM.
    /// </summary>
    /// <param name="targetFrameworkMonikerType"></param>
    /// <returns>the .NET (5+ or Core 3.1) operating system specific TFM.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown if run on an unsupported platform.</exception>
    // ReSharper disable once InconsistentNaming
    private static string GetOsSpecificNetTFM(TargetFrameworkMonikerType targetFrameworkMonikerType)
    {
        Version frameworkVersion = GetFrameworkVersion();
        
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(GetNetTFM());
        
        if (OperatingSystem.IsMacOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.TargetSystem_Mac);

            if (targetFrameworkMonikerType == TargetFrameworkMonikerType.OperatingSystemVersionSpecific)
            {
                stringBuilder.Append('.');
                stringBuilder.Append(RuntimeIdentification.GetOsVersionString());
            }
        }
        else if (OperatingSystem.IsMacCatalyst())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.TargetSystem_MacCatalyst);
        }
        else if (OperatingSystem.IsWindows())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.TargetSystem_Windows);

            if (targetFrameworkMonikerType == TargetFrameworkMonikerType.OperatingSystemVersionSpecific)
            {
                bool isAtLeastWin8 = OperatingSystem.IsWindowsVersionAtLeast(6,2, 9200);
                bool isAtLeastWin8Point1 = OperatingSystem.IsWindowsVersionAtLeast(6, 3, 9600);

                bool isAtLeastWin10V1607 = OperatingSystem.IsWindowsVersionAtLeast(10, 0, 14393);
                
                if (isAtLeastWin8 || isAtLeastWin8Point1)
                {
                    stringBuilder.Append(RuntimeIdentification.GetOsVersionString());
                }
                else if (isAtLeastWin10V1607)
                {
                    stringBuilder.Append(Environment.OSVersion.Version);
                }
                else
                {
                    throw new PlatformNotSupportedException();
                }
            }
        }
        else if (OperatingSystem.IsAndroid())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.TargetSystem_Android);
        }
        else if (OperatingSystem.IsIOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.TargetSystem_Ios);
        }
        else if (OperatingSystem.IsTvOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.TargetSystem_TvOs);
        }
        else if (OperatingSystem.IsWatchOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.TargetSystem_WatchOs);
        }
        if (frameworkVersion.Major >= 8)
        {
            if (OperatingSystem.IsBrowser())
            {
                stringBuilder.Append('-');
                stringBuilder.Append(Resources.TargetSystem_Browser);
            }
        }
        return stringBuilder.ToString();
    }

    // ReSharper disable once InconsistentNaming
    private static string GetNetCoreTFM()
    {
        Version frameworkVersion = GetFrameworkVersion();
            
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(Resources.MonikerTypes_NetCore_Full);

        stringBuilder.Append(frameworkVersion.Major);
        stringBuilder.Append('.');
        stringBuilder.Append(frameworkVersion.Minor);

        return stringBuilder.ToString();
    }

    // ReSharper disable once InconsistentNaming
    private static string GetNetFrameworkTFM()
    {
        Version frameworkVersion = GetFrameworkVersion();
            
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(Resources.MonikerTypes_Net5Plus.Remove(0, 1));
        stringBuilder.Append(frameworkVersion.Major);
        stringBuilder.Append(frameworkVersion.Minor);
                                                    
        if (frameworkVersion.Build != 0)
        {
            stringBuilder.Append(frameworkVersion.Build);
        }

        return stringBuilder.ToString();
    }

    // ReSharper disable once InconsistentNaming
    private static string GetMonoTFM()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(Resources.MonikerTypes_Mono);
            
        if (OperatingSystem.IsAndroid())
        {
            stringBuilder.Append('-');
            stringBuilder.Append("android");
        }
        else if(OperatingSystem.IsIOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append("ios");
        }
            
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets the type of Target Framework that is currently running.
    /// </summary>
    /// <returns>the type of Target Framework that is currently running.</returns>
    public static TargetFrameworkType GetFrameworkType()
    {
        string frameworkDescription = RuntimeInformation.FrameworkDescription.ToLower();
            
        Version frameworkVersion = GetFrameworkVersion();
            
        if (frameworkDescription.Contains(Resources.MonikerTypes_Mono))
        {
            return TargetFrameworkType.Mono;
        }
        else if(frameworkDescription.Contains(Resources.MonikerTypes_Framework) ||
                (frameworkVersion < new Version(5,0,0)
                 && frameworkDescription.Contains(Resources.MonikerTypes_Mono) == false
                 && frameworkDescription.Contains(Resources.MonikerTypes_NetCore) == false)){
            return TargetFrameworkType.DotNetFramework;
        }
        else
        {
            if (frameworkDescription.Contains(Resources.MonikerTypes_NetCore))
            {
                return TargetFrameworkType.DotNetCore;
            }
            
            return TargetFrameworkType.DotNet;
        }
    }
        
    /// <summary>
    /// Gets the Target Framework type and version.
    /// </summary>
    /// <returns>the target framework type and version as a tuple.</returns>
    public static (TargetFrameworkType frameworkType, Version frameworkVersion) GetFrameworkInformation()
    {
        return (GetFrameworkType(), GetFrameworkVersion());
    }
        
    /// <summary>
    /// Gets the version of the framework being used.
    /// </summary>
    /// <returns>the version of the framework being used.</returns>
    public static Version GetFrameworkVersion()
    {
        string frameworkDescription = RuntimeInformation.FrameworkDescription.ToLower();
            
        string versionString = frameworkDescription
            .Replace(Resources.MonikerTypes_Net5Plus, string.Empty)
            .Replace(Resources.MonikerTypes_NetCore, string.Empty)
            .Replace(Resources.MonikerTypes_Framework, string.Empty)
            .Replace(Resources.MonikerTypes_Mono, string.Empty)
            .Replace(Resources.MonikerTypes_Xamarin, string.Empty)
            .Replace(Resources.MonikerTypes_Maui, string.Empty)
            .Replace(" ", string.Empty);

        switch (versionString.Count(x => x == '.'))
        {
            case 3:
                break;
            case 2:
                versionString += ".0";
                break;
            case 1:
                versionString += ".0.0";
                break;
        }
            
        return Version.Parse(versionString);
    }
        
    /// <summary>
    /// Detect the Target Framework Moniker (TFM) of the currently running system.
    /// Note: This does not detect .NET Standard TFMs, UWP TFMs, Windows Phone TFMs, Silverlight TFMs, and Windows Store TFMs.
    ///
    /// </summary>
    /// <param name="targetFrameworkType">The type of TFM to generate.</param>
    /// <returns>the target framework moniker of the currently running system.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown if run on an unsupported platform.</exception>
    public static string GetTargetFrameworkMoniker(TargetFrameworkMonikerType targetFrameworkType)
    {
        TargetFrameworkType frameworkType = GetFrameworkType();
            
        if (frameworkType == TargetFrameworkType.DotNetCore)
        {
            return GetNetCoreTFM();
        }
        else if (frameworkType == TargetFrameworkType.Mono)
        {
            return GetMonoTFM();
        }
        else if (frameworkType == TargetFrameworkType.DotNetFramework)
        {
            return GetNetFrameworkTFM();
        }
        else if (frameworkType == TargetFrameworkType.DotNet)
        {
            if(targetFrameworkType == TargetFrameworkMonikerType.OperatingSystemSpecific ||
               targetFrameworkType == TargetFrameworkMonikerType.OperatingSystemVersionSpecific)
            {
                return GetOsSpecificNetTFM(targetFrameworkType);
            }
            else
            {
                return GetNetTFM();
            }
        }

        throw new PlatformNotSupportedException();
    }
}