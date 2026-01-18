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

using System.Security.AccessControl;
using System.Security.Principal;
using DotPrimitives.IO.Permissions.Windows.Helpers;

namespace DotPrimitives.IO.Permissions.Windows;

/// <summary>
/// This class provides methods to manage file and directory permissions on the Windows operating system.
///
///</summary>
/// <remarks>
/// The methods within this class are only supported on the Windows platform.
/// Attempts to use these methods on other platforms will result in a PlatformNotSupportedException.
/// </remarks>
public static class WindowsFilePermissionManager
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
    [Deprecated("5.0.0", "Use GetFilePermission(FileInfo file) instead.")]
    public static WindowsFilePermission GetFilePermission(string filePath)
    {
        if(!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException(Resources.
                Exceptions_PlatformNotSupported_RequiresOs.Replace("{targetOs}", "Windows"));
        
        FileInfo file = new(filePath);

        return GetFilePermission(file);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static WindowsFilePermission GetFilePermission(FileInfo file)
    {
        if(!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_RequiresOs.Replace("{targetOs}", "Windows"));

        ExceptionThrower.ThrowIf(!file.Exists, 
            new FileNotFoundException(Resources.Exceptions_FileNotFound.Replace("{file}", file.FullName)));

        FileSecurity fileSecurity = file.GetAccessControl(AccessControlSections.Access);

        AuthorizationRuleCollection results = fileSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
        
        return WindowsFileSystemRightsHelper.GetPermissionFromFileSecurity(results);
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
    [Deprecated("5.0.0", "Use GetDirectoryPermission(DirectoryInfo directory) instead.")]
    public static WindowsFilePermission GetDirectoryPermission(string directoryPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(directoryPath);
        
        if(!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_RequiresOs.Replace("{targetOs}", "Windows"));
        
        DirectoryInfo directory = new(directoryPath);

        return GetDirectoryPermission(directory);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    /// <exception cref="DirectoryNotFoundException"></exception>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static WindowsFilePermission GetDirectoryPermission(DirectoryInfo directory)
    {
        if(!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_RequiresOs.Replace("{targetOs}", "Windows"));

        ExceptionThrower.ThrowIf(!directory.Exists,
            new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{directory}",
                directory.Name)));
        
        DirectorySecurity directorySecurity = directory.GetAccessControl(AccessControlSections.Access);
        AuthorizationRuleCollection results = directorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
        
        return WindowsFileSystemRightsHelper.GetPermissionFromFileSecurity(results);
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
    [Deprecated("5.0.0", "Use SetFilePermission(FileInfo file, WindowsFilePermission permission) instead.")]
    public static void SetFilePermission(string filePath, WindowsFilePermission permission)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        
        if(!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_RequiresOs.Replace("{targetOs}", "Windows"));

        FileInfo file = new(filePath);
        SetFilePermission(file, permission);
    }
    
    /// <summary>
    /// Sets the Windows file permission for a given file path.
    /// </summary>
    /// <param name="file">The file to get the permissions of.</param>
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
    public static void SetFilePermission(FileInfo file, WindowsFilePermission permission)
    {
        if(!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_RequiresOs.Replace("{targetOs}", "Windows"));
        
        ExceptionThrower.ThrowIf(!file.Exists, 
            new FileNotFoundException(Resources.Exceptions_FileNotFound.Replace("{file}", file.FullName)));
        
        FileSecurity fileSecurity = file.GetAccessControl(AccessControlSections.Access);

        (FileSystemRights rights, IdentityReference identity) identityRights = WindowsFileSystemRightsHelper.GetIdentityRightsFromPermission(permission);

        fileSecurity.AddAccessRule(new(identityRights.identity, identityRights.rights, AccessControlType.Allow));
        
        file.SetAccessControl(fileSecurity);
    }

    /// <summary>
    /// Sets the Windows file permission for a given directory path.
    /// </summary>
    /// <param name="directoryPath">The directory path.</param>
    /// <param name="permission">The corresponding WindowsFilePermission enum value.</param>
    /// <exception cref="PlatformNotSupportedException">Thrown when the operating system is not Windows.</exception>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist</exception>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    [Deprecated("5.0.0", "Use SetDirectoryPermission(DirectoryInfo directory, WindowsFilePermission permission) instead.")]
    public static void SetDirectoryPermission(string directoryPath, WindowsFilePermission permission)
    {
        DirectoryInfo directory = new(directoryPath);
        
        SetDirectoryPermission(directory, permission);
    }
    
    /// <summary>
    /// Sets the Windows file permission for a given directory path.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <param name="permission">The corresponding WindowsFilePermission enum value.</param>
    /// <exception cref="PlatformNotSupportedException">Thrown when the operating system is not Windows.</exception>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist</exception>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static void SetDirectoryPermission(DirectoryInfo directory, WindowsFilePermission permission)
    {
        if(!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_RequiresOs.Replace("{targetOs}", "Windows"));
        
        ExceptionThrower.ThrowIf(!directory.Exists,
            new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{directory}",
                directory.Name)));
        
        DirectorySecurity directorySecurity = directory.GetAccessControl(AccessControlSections.Access);

        (FileSystemRights rights, IdentityReference identity) identityRights = WindowsFileSystemRightsHelper.GetIdentityRightsFromPermission(permission);

        directorySecurity.AddAccessRule(new FileSystemAccessRule(identityRights.identity, identityRights.rights, AccessControlType.Allow));
        directory.SetAccessControl(directorySecurity);
    }
}