using System.Collections.Generic;
using System.Linq;

namespace DotPrimitives.IO.Drives;

public static partial class StorageDrives
{
    private static IEnumerable<DriveInfo> EnumeratePhysicalDrivesUnix()
    {
       
    }

    private static IEnumerable<DriveInfo> EnumerateLogicalDrivesUnix()
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