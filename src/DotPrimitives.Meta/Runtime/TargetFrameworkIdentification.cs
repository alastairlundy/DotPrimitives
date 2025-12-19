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

// ReSharper disable MemberCanBePrivate.Global

using System.Linq;
using System.Text;
using DotPrimitives.Meta.Internals.Localizations;

namespace DotPrimitives.Meta.Runtime;

/// <summary>
/// A class to manage Target Framework detection
/// </summary>
public static class TargetFrameworkIdentification
{
    /// <summary>
    /// Generates a .NET (5+) generic TFM.
    /// </summary>  
    /// <returns>the .NET (5+) generic TFM.</returns>
    private static string GetNetTfm()
    {
        Version frameworkVersion = GetFrameworkVersion();
        StringBuilder stringBuilder = new StringBuilder();
        
        stringBuilder.Append(Resources.Labels_MonikerTypes_Net5Plus.Remove(0, 1));
        stringBuilder.Append(frameworkVersion.Major);
        stringBuilder.Append('.');
        stringBuilder.Append(frameworkVersion.Minor);
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Generates the .NET (5+/Core 3.1) operating system specific TFM.
    /// </summary>
    /// <param name="targetFrameworkMonikerType"></param>
    /// <returns>the .NET (5+ or Core 3.1) operating system specific TFM.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown if run on an unsupported platform.</exception>
    // ReSharper disable once InconsistentNaming
    private static string GetOsSpecificNetTFM(TargetFrameworkMonikerType targetFrameworkMonikerType)
    {
        Version frameworkVersion = GetFrameworkVersion();
        
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(GetNetTfm());
        
        if (OperatingSystem.IsMacOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.Labels_TargetSystem_Mac);

            if (targetFrameworkMonikerType == TargetFrameworkMonikerType.OperatingSystemVersionSpecific)
            {
                stringBuilder.Append('.');
                stringBuilder.Append(RuntimeIdentification.GetOsVersionString());
            }
        }
        else if (OperatingSystem.IsMacCatalyst())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.Labels_TargetSystem_MacCatalyst);
        }
        else if (OperatingSystem.IsWindows())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.Labels_TargetSystem_Windows);

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
            stringBuilder.Append(Resources.Labels_TargetSystem_Android);
        }
        else if (OperatingSystem.IsIOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.Labels_TargetSystem_Ios);
        }
        else if (OperatingSystem.IsTvOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.Labels_TargetSystem_TvOs);
        }
        else if (OperatingSystem.IsWatchOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append(Resources.Labels_TargetSystem_WatchOs);
        }
        if (frameworkVersion.Major >= 8)
        {
            if (OperatingSystem.IsBrowser())
            {
                stringBuilder.Append('-');
                stringBuilder.Append(Resources.Labels_TargetSystem_Browser);
            }
        }
        return stringBuilder.ToString();
    }

    // ReSharper disable once InconsistentNaming
    private static string GetNetCoreTFM()
    {
        Version frameworkVersion = GetFrameworkVersion();
            
        StringBuilder stringBuilder = new StringBuilder()
            .Append(Resources.Labels_MonikerTypes_Core_Full)
            .Append(frameworkVersion.Major)
            .Append('.')
            .Append(frameworkVersion.Minor);

        return stringBuilder.ToString();
    }

    private static string GetNetFrameworkTfm()
    {
        Version frameworkVersion = GetFrameworkVersion();
            
        StringBuilder stringBuilder = new StringBuilder()
            .Append(Resources.Labels_MonikerTypes_Net5Plus.ToLower()
                .Remove(0, 1))
            .Append(frameworkVersion.Major)
            .Append(frameworkVersion.Minor);
                                                    
        if (frameworkVersion.Build != 0)
        {
            stringBuilder.Append(frameworkVersion.Build);
        }

        return stringBuilder.ToString();
    }

    private static string GetMonoTfm()
    {
        StringBuilder stringBuilder = new StringBuilder()
            .Append(Resources.Labels_MonikerTypes_Mono.ToLower());
            
        if (OperatingSystem.IsAndroid())
        {
            stringBuilder.Append('-')
                .Append(Resources.Labels_TargetSystem_Android);
        }
        else if(OperatingSystem.IsIOS())
        {
            stringBuilder.Append('-')
                .Append(Resources.Labels_TargetSystem_Ios);
        }
            
        return stringBuilder.ToString();
    }
    
    private static string GetNetStandardTfm()
    {
        Version frameworkVersion = GetFrameworkVersion();
        
        StringBuilder stringBuilder = new StringBuilder();
        
        stringBuilder
            .Append(Resources.Labels_MonikerTypes_Net5Plus.ToLower().Remove(0, 1))
            .Append(Resources.Labels_MonikerTypes_NetStandard.ToLower())
            .Append(frameworkVersion.Major)
            .Append('.')
            .Append(frameworkVersion.Minor);
        
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
            
        if (frameworkDescription.Contains(Resources.Labels_MonikerTypes_Mono.ToLower()))
        {
            return TargetFrameworkType.Mono;
        }
        if (frameworkDescription.Contains(Resources.Labels_MonikerTypes_NetStandard.ToLower()))
        {
            return TargetFrameworkType.DotNetStandard;
        }
        else if(frameworkDescription.Contains(Resources.Labels_MonikerTypes_Framework.ToLower()) ||
                (frameworkVersion < new Version(5,0,0)
                 && frameworkDescription.Contains(Resources.Labels_MonikerTypes_Mono.ToLower()) == false
                 && frameworkDescription.Contains(Resources.Labels_MonikerTypes_Core.ToLower()) == false)){
            return TargetFrameworkType.DotNetFramework;
        }
        else
        {
            if (frameworkDescription.Contains(Resources.Labels_MonikerTypes_Core.ToLower()))
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
        
        string versionString = string.Join("",
                frameworkDescription.Select(x => char.IsDigit(x) || char.IsNumber(x) || x == '.'));
        
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
    /// Note: This does not detect some .NET Standard TFMs, UWP TFMs, Windows Phone TFMs, Silverlight TFMs, and Windows Store TFMs.
    ///
    /// </summary>
    /// <param name="targetFrameworkType">The type of TFM to generate.</param>
    /// <returns>the target framework moniker of the currently running system.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown if run on an unsupported platform.</exception>
    public static string GetTargetFrameworkMoniker(TargetFrameworkMonikerType targetFrameworkType)
    {
        TargetFrameworkType frameworkType = GetFrameworkType();

        switch (frameworkType)
        {
            case TargetFrameworkType.DotNetCore:
                return GetNetCoreTFM();
            case TargetFrameworkType.DotNetStandard:
                return GetNetStandardTfm();
            case TargetFrameworkType.Mono:
                return GetMonoTfm();
            case TargetFrameworkType.DotNetFramework:
                return GetNetFrameworkTfm();
            case TargetFrameworkType.DotNet:
                if(targetFrameworkType == TargetFrameworkMonikerType.OperatingSystemSpecific ||
                   targetFrameworkType == TargetFrameworkMonikerType.OperatingSystemVersionSpecific)
                {
                    return GetOsSpecificNetTFM(targetFrameworkType);
                }
                else
                {
                    return GetNetTfm();
                }
            default:
                throw new PlatformNotSupportedException();
        }
    }
}