using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DotPrimitives.Internals.Helpers;

namespace DotPrimitives.IO.Drives;

public static partial class StorageDrives
{
    [SupportedOSPlatform("macos")]
    private static IEnumerable<DriveInfo> EnumeratePhysicalDrivesMac()
    {
        if(!OperatingSystem.IsMacOS() && !OperatingSystem.IsMacCatalyst())
            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_RequiresOs.Replace("{targetOs}", "MacOS"));
        
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "/usr/bin/diskutil`",
            Arguments = "list physical | grep '^\\/dev\\/disk'"
        };
        
        ProcessWrapper wrapper = new ProcessWrapper(startInfo);
        
        wrapper.Start();

        Task<(string standardOut, string standardError)> resultsTask = wrapper.WaitForBufferedExitAsync(CancellationToken.None);

        resultsTask.Wait();
        
        string[] lines = resultsTask.Result.standardOut.Split(Environment.NewLine)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        foreach (string line in lines)
        {
            DriveInfo drive = new DriveInfo(line);
            
            yield return drive;
        }
    }

    private static IEnumerable<DriveInfo> EnumerateLogicalDrivesMac()
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