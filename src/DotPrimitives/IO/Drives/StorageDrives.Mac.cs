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
    [SupportedOSPlatform("macos")]
    private static IEnumerable<DriveInfo> EnumeratePhysicalDrivesMac()
    {
        if(!OperatingSystem.IsMacOS() && !OperatingSystem.IsMacCatalyst())
            throw new PlatformNotSupportedException(Resources.
                Exceptions_PlatformNotSupported_RequiresOs.Replace("{targetOs}", "MacOS"));
        
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "/usr/bin/diskutil`",
            Arguments = "list physical | grep '^\\/dev\\/disk'"
        };
        
        using ProcessWrapper wrapper = new ProcessWrapper(startInfo);
        
        string[] lines;

        try
        {
            wrapper.Start();

            using Task<(string standardOut, string standardError)> resultsTask = wrapper
                .WaitForBufferedExitAsync(CancellationToken.None);
        
            resultsTask.Wait();
        
            lines = resultsTask.Result.standardOut.Split(Environment.NewLine)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }
        catch
        {
            yield break;
        }

        foreach (string line in lines)
        {
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