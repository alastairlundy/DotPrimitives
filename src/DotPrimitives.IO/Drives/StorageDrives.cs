using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;

namespace DotPrimitives.IO.Drives;

/// <summary>
/// 
/// </summary>
public static partial class StorageDrives
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    public static IEnumerable<DriveInfo> EnumeratePhysicalDrives()
    {
        if (OperatingSystem.IsWindows())
            return EnumeratePhysicalDrivesWindows();
        
        if (OperatingSystem.IsMacCatalyst() || OperatingSystem.IsMacOS())
            return EnumeratePhysicalDrivesMac();

        if (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD() || OperatingSystem.IsAndroid())
            return EnumeratePhysicalDrivesUnix();

        throw new PlatformNotSupportedException();
    }

    /// <summary>
    /// Retrieves an array of all physical drives available on the system.
    /// A physical drive is either a fixed drive, removable drive, or CD/DVD drive
    /// that a computer or device has initialized and is ready for use.
    /// </summary>
    /// <returns>
    /// An array of <see cref="System.IO.DriveInfo"/> objects representing the physical drives
    /// available and ready for access on the system. Returns an empty array if no drives are found.
    /// </returns>
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    public static DriveInfo[] GetPhysicalDrives()
        => EnumeratePhysicalDrives().ToArray();

    /// <summary>
    /// Enumerates all logical drives available on the current platform.
    /// Logical drives represent the accessible storage volumes configured on the system.
    /// </summary>
    /// <returns>
    /// An enumerable collection of <see cref="System.IO.DriveInfo"/> objects representing the logical drives
    /// accessible on the system. Throws <see cref="PlatformNotSupportedException"/> if the platform is not supported.
    /// </returns>
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    public static IEnumerable<DriveInfo> EnumerateLogicalDrives()
    {
        if (OperatingSystem.IsWindows())
            return EnumerateLogicalDrivesWindows();

        if (OperatingSystem.IsMacCatalyst() || OperatingSystem.IsMacOS())
            return EnumerateLogicalDrivesMac();

        if (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD() || OperatingSystem.IsAndroid())
            return EnumerateLogicalDrivesUnix();

        throw new PlatformNotSupportedException();
    }

    /// <summary>
    /// Retrieves an array of all logical drives available on the system.
    /// A logical drive represents a partition or volume that is accessible
    /// as a storage unit, such as those identified by drive letters on Windows
    /// or mount points on Unix-like operating systems.
    /// </summary>
    /// <returns>
    /// An array of <see cref="System.IO.DriveInfo"/> objects representing the logical drives
    /// available on the system. Returns an empty array if no logical drives are found.
    /// </returns>
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    public static DriveInfo[] GetLogicalDrives()
        => EnumerateLogicalDrives().ToArray();
}