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