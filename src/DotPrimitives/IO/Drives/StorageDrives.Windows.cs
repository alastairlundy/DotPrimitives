using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DotPrimitives.Internals.Helpers;

namespace DotPrimitives.IO.Drives;

public static partial class StorageDrives
{
    [SupportedOSPlatform("windows")]
    private static IEnumerable<DriveInfo> EnumeratePhysicalDrivesWindows()
    {
        string winPowershell =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.Windows)}/System32/WindowsPowerShell/v1.0/powershell.exe";
        
        string programFilesDir = Environment.Is64BitOperatingSystem ? 
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) :
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        
        string? powershell5Plus = Directory.EnumerateFiles(programFilesDir,
                "pwsh.exe", SearchOption.AllDirectories)
            .Where(d =>
            {
                try
                {
                    return d.ToLower().Contains("powershell");
                }
                catch
                {
                    // ignored
                    return false;
                }
            })
            .FirstOrDefault();
        
        if(!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException(Resources.
                Exceptions_PlatformNotSupported_RequiresOs.Replace("{targetOs}", "Windows"));
        
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = powershell5Plus ?? winPowershell,
            Arguments = "Get-WMIObject Win32_LogicalDisk | Select DeviceID, DriveType"
        };
        
        ProcessWrapper wrapper = new ProcessWrapper(startInfo);
        
        wrapper.Start();

        Task<(string standardOut, string standardError)> resultsTask = wrapper.WaitForBufferedExitAsync(
            CancellationToken.None);

        resultsTask.Wait();
        
        string[] lines = resultsTask.Result.standardOut.Split(Environment.NewLine)
            .Where(x => !string.IsNullOrWhiteSpace(x) && !x.ToLower().Contains("deviceid") &&
                        !x.ToLower().Contains("--"))
            .Select(x => x.TrimEnd(':'))
            .ToArray();

        foreach (string line in lines)
        {
            if (!line.Contains('3')) 
                continue;
            
            DriveInfo drive = new DriveInfo(line);
            
            yield return drive;
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