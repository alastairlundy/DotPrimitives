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

namespace DotPrimitives.IO.Permissions.Windows;

/// <summary>
/// Represents the set of file system permissions available on Windows.
/// </summary>
[Flags]
public enum WindowsFilePermission
{
    /// <summary>
    /// Grants the system user full control over a file or directory, including the ability to read, write, modify, and manage permissions.
    /// </summary>
    SystemFullControl,
    /// <summary>
    /// Grants the system user the ability to modify a file or directory.
    /// </summary>
    SystemModify,
    /// <summary>
    /// Grants the system user the ability to read a file or directory.
    /// </summary>
    SystemRead,
    /// <summary>
    /// Grants the system user the ability to write to a file or directory.
    /// </summary>
    SystemWrite,
    /// <summary>
    /// Grants the system user the ability to read and execute a file or directory.
    /// </summary>
    SystemReadAndExecute,
    /// <summary>
    /// Grants the system user the ability to view the names of files and subdirectories within a folder.
    /// </summary>
    SystemListFolderContents,
    /// <summary>
    /// Grants the user full control over a file or directory, including read, write, modify, and execute permissions.
    /// </summary>
    UserFullControl,
    /// <summary>
    /// Grants a standard user the ability to modify a file or directory.
    /// </summary>
    UserModify,
    /// <summary>
    /// Grants a user the ability to read a file or directory.
    /// </summary>
    UserRead,
    /// <summary>
    /// Grants the user the ability to write to a file or directory.
    /// </summary>
    UserWrite,
    /// <summary>
    /// Grants a user the ability to read a file or directory and execute it,
    /// if the file is an executable or the directory contains executable files.
    /// </summary>
    UserReadAndExecute,
    /// <summary>
    /// Grants the user the ability to view the contents of a folder without allowing any other access rights.
    /// </summary>
    UserListFolderContents,
    /// <summary>
    /// Grants a group of users full control over a file or directory, including the ability to read, write, execute, and modify permissions.
    /// </summary>
    GroupFullControl,
    /// <summary>
    /// Grants a group of users the ability to modify a file or directory.
    /// </summary>
    GroupModify,
    /// <summary>
    /// Grants members of the group the ability to read a file or directory.
    /// </summary>
    GroupRead,
    /// <summary>
    /// Grants the group users the ability to write to a file or directory.
    /// </summary>
    GroupWrite,
    /// <summary>
    /// Grants a group the ability to read and execute a file or traverse a directory.
    /// </summary>
    GroupReadAndExecute,
    /// <summary>
    /// Grants the group users the ability to view the contents of a folder without modifying it.
    /// </summary>
    GroupListFolderContents,
}