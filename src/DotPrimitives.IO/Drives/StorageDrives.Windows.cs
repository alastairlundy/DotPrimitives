using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;

namespace DotPrimitives.IO.Drives;

public static partial class StorageDrives
{
    [SupportedOSPlatform("windows")]
    private static IEnumerable<DriveInfo> EnumeratePhysicalDrivesWindows()
    {
        return DriveInfo.GetDrives()
            .Where(d => d.IsReady)
            .Where(d => !d.DriveType.HasFlag(DriveType.Unknown) && !d.DriveType.HasFlag(DriveType.Ram))
            .Where(d => d.DriveType.HasFlag(DriveType.Fixed) || d.DriveType.HasFlag(DriveType.Removable) ||
                        d.DriveType.HasFlag(DriveType.CDRom))
            .Where(d =>
            {
                try
                {
                    return d.TotalSize > 1024;
                }
                catch
                {
                    return false;
                }
            });
    }

    [SupportedOSPlatform("windows")]
    private static IEnumerable<DriveInfo> EnumerateLogicalDrivesWindows()
    {
        return DriveInfo.GetDrives()
            .Where(d => d.IsReady)
            .Where(d => !d.DriveType.HasFlag(DriveType.Unknown))
            .Where(d => !d.DriveType.HasFlag(DriveType.NoRootDirectory) && !d.DriveType.HasFlag(DriveType.Ram))
            .Where(d =>
            {
                try
                {
                    return d.TotalSize > 0;
                }
                catch
                {
                    return false;
                }
            });
    }
}