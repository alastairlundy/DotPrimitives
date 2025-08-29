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
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Security.Principal;

using AlastairLundy.DotPrimitives.IO.Permissions.Windows.Helpers;

namespace AlastairLundy.DotPrimitives.IO.Permissions.Windows;

/// <summary>
/// 
/// </summary>
public static class WindowsFilePermissionDetector
{
    /// <summary>
    /// Gets the Windows file permission for a given file path.
    /// </summary>
    /// <param name="filePath">The path of the file.</param>
    /// <returns>The corresponding WindowsFilePermission enum value.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown when the operating system is not Windows.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the specified file does not exist.</exception>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static WindowsFilePermission GetFilePermission(string filePath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == false)
            throw new PlatformNotSupportedException();

        if (File.Exists(filePath) == false)
            throw new FileNotFoundException();
        
        FileInfo file = new FileInfo(filePath);

        FileSecurity fileSecurity = file.GetAccessControl(AccessControlSections.Access);

        AuthorizationRuleCollection results = fileSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
        
        return FileSystemRightsHelper.GetPermissionFromFileSecurity(results);
    }

    /// <summary>
    /// Gets the Windows file permission for a given directory path.
    /// </summary>
    /// <param name="directoryPath">The path of the directory.</param>
    /// <returns>The corresponding WindowsFilePermission enum value representing the directory's permissions.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown when the operating system is not Windows.</exception>
    /// <exception cref="DirectoryNotFoundException">Thrown if the specified directory does not exist.</exception>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static WindowsFilePermission GetDirectoryPermission(string directoryPath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == false)
            throw new PlatformNotSupportedException();

        if (Directory.Exists(directoryPath) == false)
            throw new DirectoryNotFoundException();
        
        DirectoryInfo directory = new DirectoryInfo(directoryPath);

        DirectorySecurity directorySecurity = directory.GetAccessControl(AccessControlSections.Access);
        AuthorizationRuleCollection results = directorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
        
        return FileSystemRightsHelper.GetPermissionFromFileSecurity(results);
    }

    /// <summary>
    /// Sets the Windows file permission for a given file path.
    /// </summary>
    /// <param name="filePath">The file path of the file.</param>
    /// <param name="permission">The corresponding WindowsFilePermission enum value.</param>
    /// <exception cref="PlatformNotSupportedException">Thrown when the operating system is not Windows.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the specified file does not exist.</exception>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static void SetFilePermission(string filePath, WindowsFilePermission permission)
    {
        if (filePath == null) throw new ArgumentNullException(nameof(filePath));
#if NET5_0_OR_GREATER
        if (OperatingSystem.IsWindows() == false)
#else
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == false)
#endif
            throw new PlatformNotSupportedException(); 
        
        if (File.Exists(filePath) == false)
            throw new FileNotFoundException();
        
        FileInfo file = new FileInfo(filePath);
        FileSecurity fileSecurity = file.GetAccessControl(AccessControlSections.Access);

        (FileSystemRights rights, IdentityReference identity) identityRights = FileSystemRightsHelper.GetIdentityRightsFromPermission(permission);

        fileSecurity.AddAccessRule(new FileSystemAccessRule(identityRights.identity, identityRights.rights, AccessControlType.Allow));
        
        file.SetAccessControl(fileSecurity);
    }

    /// <summary>
    /// Sets the Windows file permission for a given directory path.
    /// </summary>
    /// <param name="directoryPath">The directory path.</param>
    /// <param name="permission">The corresponding WindowsFilePermission enum value.</param>
    /// <exception cref="PlatformNotSupportedException">Thrown when the operating system is not Windows.</exception>
    /// <exception cref="DirectoryNotFoundException"></exception>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static void SetDirectoryPermission(string directoryPath, WindowsFilePermission permission)
    {
#if NET5_0_OR_GREATER
        if (OperatingSystem.IsWindows() == false)
#else
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == false)
#endif
            throw new PlatformNotSupportedException(); 
        
        if (Directory.Exists(directoryPath) == false)
            throw new DirectoryNotFoundException();
        
        DirectoryInfo directory = new DirectoryInfo(directoryPath);
        DirectorySecurity directorySecurity = directory.GetAccessControl(AccessControlSections.Access);

        (FileSystemRights rights, IdentityReference identity) identityRights = FileSystemRightsHelper.GetIdentityRightsFromPermission(permission);

        directorySecurity.AddAccessRule(new FileSystemAccessRule(identityRights.identity, identityRights.rights, AccessControlType.Allow));
        
        directory.SetAccessControl(directorySecurity);
    }
}