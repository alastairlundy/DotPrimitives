/*
    AlastairLundy.DotPrimitives
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.IO;
using System.Runtime.InteropServices;

using System.Runtime.Versioning;

using System.Security.AccessControl;
using System.Security.Principal;

namespace AlastairLundy.DotPrimitives.IO.Permissions;

/// <summary>
/// 
/// </summary>
public class WindowsFilePermissionDetector
{
    
    
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static WindowsFilePermission GetWindowsFilePermission(string path)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == false)
            throw new PlatformNotSupportedException(); 
        
        FileInfo file = new FileInfo(path);

        FileSecurity fileSecurity = file.GetAccessControl(AccessControlSections.Access);

        IdentityReference systemIdentity =  new SecurityIdentifier(WellKnownSidType.WinSystemLabelSid, null);
        IdentityReference groupIdentity = new SecurityIdentifier(WellKnownSidType.CreatorGroupSid, null);
        
        WindowsFilePermission permission = WindowsFilePermission.SystemRead;
        
        foreach (FileSystemAccessRule rule in fileSecurity.GetAccessRules(true,
                     true, typeof(SecurityIdentifier)))
        {
            if (rule.IdentityReference == systemIdentity)
            {
                switch (rule.FileSystemRights)
                {
                    case FileSystemRights.FullControl or FileSystemRights.ChangePermissions or FileSystemRights.TakeOwnership:
                        permission = permission | WindowsFilePermission.SystemFullControl;
                        break;
                    case FileSystemRights.Modify or FileSystemRights.AppendData:
                        permission = permission | WindowsFilePermission.SystemModify;
                        break;
                    case FileSystemRights.Read or FileSystemRights.ReadPermissions or
                        FileSystemRights.ReadAttributes or FileSystemRights.ReadExtendedAttributes:
                        permission = permission | WindowsFilePermission.SystemRead;
                        break;
                    case FileSystemRights.Write or FileSystemRights.WriteData or FileSystemRights.WriteAttributes
                        or FileSystemRights.WriteExtendedAttributes or FileSystemRights.CreateFiles
                        or FileSystemRights.CreateDirectories:
                        permission = permission | WindowsFilePermission.SystemWrite;
                        break;
                    case FileSystemRights.ListDirectory:
                        permission = permission | WindowsFilePermission.SystemListFolderContents;
                        break;
                    case FileSystemRights.ReadAndExecute:
                        permission = permission | WindowsFilePermission.SystemReadAndExecute;
                        break;
                    case FileSystemRights.Delete or FileSystemRights.DeleteSubdirectoriesAndFiles:
                        permission = permission | WindowsFilePermission.SystemRead | WindowsFilePermission.SystemWrite;
                        break;
                    case FileSystemRights.ExecuteFile or FileSystemRights.Synchronize:
                        permission = permission | WindowsFilePermission.SystemReadAndExecute;
                        break;
                }  
            }

            if (rule.IdentityReference == groupIdentity)
            {
                 switch (rule.FileSystemRights)
                {
                    case FileSystemRights.FullControl or FileSystemRights.ChangePermissions or FileSystemRights.TakeOwnership:
                        permission = permission | WindowsFilePermission.GroupFullControl;
                        break;
                    case FileSystemRights.Modify or FileSystemRights.AppendData:
                        permission = permission | WindowsFilePermission.GroupModify;
                        break;
                    case FileSystemRights.Read or FileSystemRights.ReadPermissions or
                        FileSystemRights.ReadAttributes or FileSystemRights.ReadExtendedAttributes:
                        permission = permission | WindowsFilePermission.GroupRead;
                        break;
                    case FileSystemRights.Write or FileSystemRights.WriteData or FileSystemRights.WriteAttributes
                        or FileSystemRights.WriteExtendedAttributes or FileSystemRights.CreateFiles
                        or FileSystemRights.CreateDirectories:
                        permission = permission | WindowsFilePermission.GroupWrite;
                        break;
                    case FileSystemRights.ListDirectory:
                        permission = permission | WindowsFilePermission.GroupListFolderContents;
                        break;
                    case FileSystemRights.ReadAndExecute:
                        permission = permission | WindowsFilePermission.GroupReadAndExecute;
                        break;
                    case FileSystemRights.Delete or FileSystemRights.DeleteSubdirectoriesAndFiles:
                        permission = permission | WindowsFilePermission.GroupRead | WindowsFilePermission.GroupWrite;
                        break;
                    case FileSystemRights.ExecuteFile or FileSystemRights.Synchronize:
                        permission = permission | WindowsFilePermission.GroupReadAndExecute;
                        break;
                }  
            }
        }
        
        return permission;
    }

    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    public static void SetWindowsFilePermission(string path, WindowsFilePermission permission)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == false)
            throw new PlatformNotSupportedException(); 
        
        FileInfo file = new FileInfo(path);
        
        FileSecurity fileSecurity = file.GetAccessControl(AccessControlSections.Access);

        IdentityReference userIdentity = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
        IdentityReference groupIdentity = new SecurityIdentifier(WellKnownSidType.CreatorGroupSid, null);
        IdentityReference systemIdentity =  new SecurityIdentifier(WellKnownSidType.WinSystemLabelSid, null);
        
        IdentityReference identity;
        FileSystemRights rights = FileSystemRights.ListDirectory;
        
        switch (permission)
        {
            case WindowsFilePermission.SystemRead:
                rights = rights | FileSystemRights.Read;
                identity = systemIdentity;
                break;
            case WindowsFilePermission.SystemWrite:
                rights = rights | FileSystemRights.Write;
                identity = systemIdentity;
                break;
            case WindowsFilePermission.SystemReadAndExecute:
                rights = rights | FileSystemRights.ReadAndExecute;
                identity = systemIdentity;
                break;
            case WindowsFilePermission.SystemFullControl:
                rights = rights | FileSystemRights.FullControl;
                identity = systemIdentity;
                break;
            case WindowsFilePermission.SystemModify:
                rights = rights | FileSystemRights.Modify;
                identity = systemIdentity;
                break;
            case WindowsFilePermission.SystemListFolderContents:
                rights = rights | FileSystemRights.ListDirectory;
                identity = systemIdentity;
                break;
            case WindowsFilePermission.GroupRead:
                rights = rights | FileSystemRights.Read;
                identity = groupIdentity;
                break;
            case WindowsFilePermission.GroupWrite:
                rights = rights | FileSystemRights.Write;
                identity = groupIdentity;
                break;
            case WindowsFilePermission.GroupReadAndExecute:
                rights = rights | FileSystemRights.ReadAndExecute;
                identity = groupIdentity;
                break;
            case WindowsFilePermission.GroupFullControl:
                rights = rights | FileSystemRights.FullControl;
                identity = groupIdentity;
                break;
            case WindowsFilePermission.GroupModify:
                rights = rights | FileSystemRights.Modify;
                identity = groupIdentity;
                break;
            case WindowsFilePermission.GroupListFolderContents:
                rights = rights | FileSystemRights.ListDirectory;
                identity = groupIdentity;
                break;
            case WindowsFilePermission.UserRead:
                rights = rights | FileSystemRights.Read;
                identity = userIdentity;
                break;
            case WindowsFilePermission.UserWrite:
                rights = rights | FileSystemRights.Write;
                identity = userIdentity;
                break;
            case WindowsFilePermission.UserReadAndExecute:
                rights = rights | FileSystemRights.ReadAndExecute;
                identity = userIdentity;
                break;
            case WindowsFilePermission.UserFullControl:
                rights = rights | FileSystemRights.FullControl;
                identity = userIdentity;
                break;
            case WindowsFilePermission.UserModify:
                rights = rights | FileSystemRights.Modify;
                identity = userIdentity;
                break;
            case WindowsFilePermission.UserListFolderContents:
                rights = rights | FileSystemRights.ListDirectory;
                identity = userIdentity;
                break;
            default:
                rights = rights | FileSystemRights.Read;
                identity = new SecurityIdentifier(WellKnownSidType.WinUntrustedLabelSid, null);
                break;
        }
        
        fileSecurity.AddAccessRule(new FileSystemAccessRule(identity, rights, AccessControlType.Allow));
        
        file.SetAccessControl(fileSecurity);
    }
}