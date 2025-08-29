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

using System;
using System.IO;
using System.Runtime.Versioning;

#if NETSTANDARD2_0
using OperatingSystem = Polyfills.OperatingSystemPolyfill;
#endif

namespace AlastairLundy.DotPrimitives.IO.Permissions.Windows;

public static class WindowsFilePermissionsFromInfoExtensions
{
    /// <summary>
    /// Retrieves the Windows file permission for a given FileInfo object.
    /// </summary>
    /// <param name="fileInfo">The FileInfo object for which to retrieve the permission.</param>
    /// <returns>A WindowsFilePermission indicating the permission of the specified file or directory.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown when the operation is performed on a platform that is not Windows based.</exception>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static WindowsFilePermission GetFilePermission(this FileInfo fileInfo)
    {
        if(OperatingSystem.IsWindows() == false)
            throw new PlatformNotSupportedException();
        
        return WindowsFilePermissionDetector.GetFilePermission(fileInfo.FullName);
    }

    /// <summary>
    /// Retrieves the Windows directory permission for a given DirectoryInfo object.
    /// </summary>
    /// <param name="directoryInfo">The DirectoryInfo object for which to retrieve the permission.</param>
    /// <returns>A WindowsFilePermission indicating the permission of the specified directory.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown when the operation is performed on a platform that is not Windows based.</exception>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static WindowsFilePermission GetDirectoryPermission(this DirectoryInfo directoryInfo)
    {
        if(OperatingSystem.IsWindows() == false)
            throw new PlatformNotSupportedException();
        
        return WindowsFilePermissionDetector.GetDirectoryPermission(directoryInfo.FullName);
    }

    /// <summary>
    /// Sets the Windows file permission for a given FileInfo object.
    /// </summary>
    /// <param name="fileInfo">The FileInfo object for which to set the permission.</param>
    /// <param name="permission">A WindowsFilePermission indicating the new permission of the specified file or directory.</param>
    /// <exception cref="PlatformNotSupportedException">Thrown when the operation is performed on a platform that is not Windows based.</exception>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static void SetFilePermission(this FileInfo fileInfo, WindowsFilePermission permission)
    {
        if(OperatingSystem.IsWindows() == false)
            throw new PlatformNotSupportedException();

        WindowsFilePermissionDetector.SetFilePermission(fileInfo.FullName, permission);
    }

    /// <summary>
    /// Sets the Windows file permission for a given DirectoryInfo object.
    /// </summary>
    /// <param name="directoryInfo">The DirectoryInfo object for which to set the permission.</param>
    /// <param name="permission">The WindowsFilePermission to be assigned.</param>
    /// <exception cref="PlatformNotSupportedException">Thrown when the operation is performed on a platform that is not Windows based.</exception>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static void SetDirectoryPermission(this DirectoryInfo directoryInfo, WindowsFilePermission permission)
    {
        if(OperatingSystem.IsWindows() == false)
            throw new PlatformNotSupportedException();

        WindowsFilePermissionDetector.SetDirectoryPermission(directoryInfo.FullName, permission);
    }
}