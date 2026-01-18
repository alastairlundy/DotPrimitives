/*
    MIT License
   
    Copyright (c) 2025-2026 Alastair Lundy
   
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

namespace DotPrimitives.IO.Drives;

public static partial class StorageDrives
{
    [SupportedOSPlatform("windows")]
    private static IEnumerable<DriveInfo> EnumeratePhysicalDrivesWindows()
    {
        if(!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException(Resources.
                Exceptions_PlatformNotSupported_RequiresOs.Replace("{targetOs}", "Windows"));

        string winPowershell =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.Windows)}/System32/WindowsPowerShell/v1.0/powershell.exe";
        
#if NET8_0_OR_GREATER
        string programFilesDir = Environment.Is64BitOperatingSystem ? 
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) :
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

        string?powershell5Plus = Directory.EnumerateFiles(programFilesDir, "*", new EnumerationOptions
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = true,
                MatchCasing = MatchCasing.CaseInsensitive
            })
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
#else
        string? powershell5Plus = ExecutableFinder.FindExecutable("pwsh.exe");
#endif

        
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = powershell5Plus ?? winPowershell,
            Arguments = "Get-WMIObject Win32_LogicalDisk | Select DeviceID, DriveType"
        };
        
        using ProcessWrapper wrapper = new ProcessWrapper(startInfo);
        
        string[] lines;

        try
        {
            wrapper.Start();

            using Task<(string standardOut, string standardError)> resultsTask = wrapper.WaitForBufferedExitAsync(
                CancellationToken.None);

            resultsTask.Wait();

            lines = resultsTask.Result.standardOut.Split(Environment.NewLine)
                .Where(x => !string.IsNullOrWhiteSpace(x) && !x.ToLower().Contains("deviceid") &&
                            !x.ToLower().Contains("--"))
                .Select(x => x.TrimEnd(':'))
                .ToArray();
        }
        catch
        {
            yield break;
        }

        foreach (string line in lines)
        {
            if (!line.Contains('3')) 
                continue;
            
            DriveInfo? drive;

            try
            {
                drive = new DriveInfo(line);
            }
            catch
            {
                drive = null;
            }
            
            if(drive is not null)
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