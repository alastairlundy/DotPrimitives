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

using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Security.Principal;

namespace AlastairLundy.DotPrimitives.IO.Permissions.Windows.Helpers;

internal class FileSystemRightsHelper
{
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    internal static WindowsFilePermission GetPermissionFromFileSecurity(AuthorizationRuleCollection accessRules)
    {
        IdentityReference systemIdentity =  new SecurityIdentifier(WellKnownSidType.WinSystemLabelSid, null);
        IdentityReference groupIdentity = new SecurityIdentifier(WellKnownSidType.CreatorGroupSid, null);

        WindowsFilePermission permission = WindowsFilePermission.SystemRead;
        
        foreach (FileSystemAccessRule rule in accessRules)
        {
            if (rule.IdentityReference == systemIdentity)
            {
                switch (rule.FileSystemRights)
                {
                    case FileSystemRights.FullControl or FileSystemRights.ChangePermissions or FileSystemRights.TakeOwnership:
                        permission |= WindowsFilePermission.SystemFullControl;
                        break;
                    case FileSystemRights.Modify or FileSystemRights.AppendData:
                        permission |= WindowsFilePermission.SystemModify;
                        break;
                    case FileSystemRights.Read or FileSystemRights.ReadPermissions or
                        FileSystemRights.ReadAttributes or FileSystemRights.ReadExtendedAttributes:
                        permission |= WindowsFilePermission.SystemRead;
                        break;
                    case FileSystemRights.Write or FileSystemRights.WriteData or FileSystemRights.WriteAttributes
                        or FileSystemRights.WriteExtendedAttributes or FileSystemRights.CreateFiles
                        or FileSystemRights.CreateDirectories:
                        permission |= WindowsFilePermission.SystemWrite;
                        break;
                    case FileSystemRights.ListDirectory:
                        permission |= WindowsFilePermission.SystemListFolderContents;
                        break;
                    case FileSystemRights.ReadAndExecute:
                        permission |= WindowsFilePermission.SystemReadAndExecute;
                        break;
                    case FileSystemRights.Delete or FileSystemRights.DeleteSubdirectoriesAndFiles:
                        permission = permission | WindowsFilePermission.SystemRead | WindowsFilePermission.SystemWrite;
                        break;
                    case FileSystemRights.ExecuteFile or FileSystemRights.Synchronize:
                        permission |= WindowsFilePermission.SystemReadAndExecute;
                        break;
                }  
            }

            if (rule.IdentityReference == groupIdentity)
            {
                switch (rule.FileSystemRights)
                {
                    case FileSystemRights.FullControl or FileSystemRights.ChangePermissions or FileSystemRights.TakeOwnership:
                        permission |= WindowsFilePermission.GroupFullControl;
                        break;
                    case FileSystemRights.Modify or FileSystemRights.AppendData:
                        permission |= WindowsFilePermission.GroupModify;
                        break;
                    case FileSystemRights.Read or FileSystemRights.ReadPermissions or
                        FileSystemRights.ReadAttributes or FileSystemRights.ReadExtendedAttributes:
                        permission |= WindowsFilePermission.GroupRead;
                        break;
                    case FileSystemRights.Write or FileSystemRights.WriteData or FileSystemRights.WriteAttributes
                        or FileSystemRights.WriteExtendedAttributes or FileSystemRights.CreateFiles
                        or FileSystemRights.CreateDirectories:
                        permission |= WindowsFilePermission.GroupWrite;
                        break;
                    case FileSystemRights.ListDirectory:
                        permission |= WindowsFilePermission.GroupListFolderContents;
                        break;
                    case FileSystemRights.ReadAndExecute:
                        permission |= WindowsFilePermission.GroupReadAndExecute;
                        break;
                    case FileSystemRights.Delete or FileSystemRights.DeleteSubdirectoriesAndFiles:
                        permission = permission | WindowsFilePermission.GroupRead | WindowsFilePermission.GroupWrite;
                        break;
                    case FileSystemRights.ExecuteFile or FileSystemRights.Synchronize:
                        permission |= WindowsFilePermission.GroupReadAndExecute;
                        break;
                }  
            }
        }
        
        return permission;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("macos")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("ios")]
    internal static (FileSystemRights rights, IdentityReference identity) GetIdentityRightsFromPermission (WindowsFilePermission permission)
    {
        IdentityReference userIdentity = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
        IdentityReference groupIdentity = new SecurityIdentifier(WellKnownSidType.CreatorGroupSid, null);
        IdentityReference systemIdentity =  new SecurityIdentifier(WellKnownSidType.WinSystemLabelSid, null);
        
        IdentityReference identity;
        
        FileSystemRights rights = FileSystemRights.ListDirectory;
        
        switch (permission)
        {
            case WindowsFilePermission.SystemRead:
                rights |= FileSystemRights.Read;
                identity = systemIdentity;
                break;
            case WindowsFilePermission.SystemWrite:
                rights |= FileSystemRights.Write;
                identity = systemIdentity;
                break;
            case WindowsFilePermission.SystemReadAndExecute:
                rights |= FileSystemRights.ReadAndExecute;
                identity = systemIdentity;
                break;
            case WindowsFilePermission.SystemFullControl:
                rights |= FileSystemRights.FullControl;
                identity = systemIdentity;
                break;
            case WindowsFilePermission.SystemModify:
                rights |= FileSystemRights.Modify;
                identity = systemIdentity;
                break;
            case WindowsFilePermission.SystemListFolderContents:
                rights |= FileSystemRights.ListDirectory;
                identity = systemIdentity;
                break;
            case WindowsFilePermission.GroupRead:
                rights |= FileSystemRights.Read;
                identity = groupIdentity;
                break;
            case WindowsFilePermission.GroupWrite:
                rights |= FileSystemRights.Write;
                identity = groupIdentity;
                break;
            case WindowsFilePermission.GroupReadAndExecute:
                rights |= FileSystemRights.ReadAndExecute;
                identity = groupIdentity;
                break;
            case WindowsFilePermission.GroupFullControl:
                rights |= FileSystemRights.FullControl;
                identity = groupIdentity;
                break;
            case WindowsFilePermission.GroupModify:
                rights |= FileSystemRights.Modify;
                identity = groupIdentity;
                break;
            case WindowsFilePermission.GroupListFolderContents:
                rights |= FileSystemRights.ListDirectory;
                identity = groupIdentity;
                break;
            case WindowsFilePermission.UserRead:
                rights |= FileSystemRights.Read;
                identity = userIdentity;
                break;
            case WindowsFilePermission.UserWrite:
                rights |= FileSystemRights.Write;
                identity = userIdentity;
                break;
            case WindowsFilePermission.UserReadAndExecute:
                rights |= FileSystemRights.ReadAndExecute;
                identity = userIdentity;
                break;
            case WindowsFilePermission.UserFullControl:
                rights |= FileSystemRights.FullControl;
                identity = userIdentity;
                break;
            case WindowsFilePermission.UserModify:
                rights |= FileSystemRights.Modify;
                identity = userIdentity;
                break;
            case WindowsFilePermission.UserListFolderContents:
                rights |= FileSystemRights.ListDirectory;
                identity = userIdentity;
                break;
            default:
                rights |= FileSystemRights.Read;
                identity = new SecurityIdentifier(WellKnownSidType.WinUntrustedLabelSid, null);
                break;
        }

        return (rights, identity);
    }
}