using WmiLight;

namespace DotPrimitives.IO.Drives;

public static partial class StorageDrives
{
    [SupportedOSPlatform("windows")]
    private static IEnumerable<DriveInfo> EnumeratePhysicalDrivesWindows()
    {
        using WmiConnection connection = new WmiConnection();
        
        foreach (WmiObject logicalDrive in connection.CreateQuery("SELECT * FROM Win32_LogicalDisk"))
        {
            if (uint.TryParse(logicalDrive["DriveType"].ToString(), out uint type))
            {
                if (type != 3 && type != 4)
                    continue;

                string? driveLetter = logicalDrive["Name"] as string;

                string? volumeLabel = logicalDrive["VolumeName"] as string;
                    
                if(driveLetter is null)
                    continue;

                DriveInfo driveInfo = new(driveLetter);

                if (volumeLabel is not null)
                    driveInfo.VolumeLabel = volumeLabel;

                yield return driveInfo;
            }
        }
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