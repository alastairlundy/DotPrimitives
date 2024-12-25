/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Resyslib.IO.Directories;

public class DirectoryCreator : IDirectoryCreator
{
    /// <summary>
    /// Attempts to create a new directory with the specified parameters.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to be created.</param>
    /// <param name="newDirectoryName"></param>
    /// <param name="unixFileMode">The file mode to use to create the directory. Only used on Unix based systems.</param>
    /// <param name="createParentPaths">Whether to create parent directory paths, if required, when creating the new directory.</param>
    /// <returns>true if the directory was successfully created; returns false otherwise.</returns>
    public bool TryCreateDirectory(string directoryPath, string newDirectoryName, UnixFileMode unixFileMode, bool createParentPaths)
    {
        try
        {
            CreateDirectory(directoryPath, newDirectoryName, unixFileMode, createParentPaths);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <param name="unixFileMode"></param>
    /// <returns></returns>
    public bool TryCreateParentDirectory(string directoryPath, UnixFileMode unixFileMode)
    {
        try
        {
            CreateParentDirectory(directoryPath, unixFileMode);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool TryCreateParentDirectory(string directoryPath)
    {
        try
        {
            CreateParentDirectory(directoryPath);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Recursively creates the parent directory as needed.
    /// </summary>
    /// <param name="parentDirectory">The parent directory to be created.</param>
    /// <param name="unixFileMode"></param>
    public void CreateParentDirectory(string parentDirectory, UnixFileMode unixFileMode)
    {
        string[] directories = parentDirectory.Split(Path.DirectorySeparatorChar);

        List<string> directoriesToCreate = new List<string>();
        
        for (int i = 0; i < directories.Length; i++)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int j = 0; j < i; j++)
            {
                stringBuilder.Append(directories[i][j]);
            }
            
            directoriesToCreate.Add(stringBuilder.ToString());
        }
        
        foreach (string directory in directoriesToCreate)
        {
            if (Directory.Exists(directory) == false)
            {
                CreateDirectory(directory, directory, unixFileMode, false);
            }
        }
    }
    
    public void CreateParentDirectory(string parentDirectory)
    {
        string[] directories = parentDirectory.Split(Path.DirectorySeparatorChar);

        List<string> directoriesToCreate = new List<string>();
        
        for (int i = 0; i < directories.Length; i++)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int j = 0; j < i; j++)
            {
                stringBuilder.Append(directories[i][j]);
            }
            
            directoriesToCreate.Add(stringBuilder.ToString());
        }
        
        foreach (string directory in directoriesToCreate)
        {
            if (Directory.Exists(directory) == false)
            {
                CreateDirectory(directory, directory, false);
            }
        }
    }

    /// <summary>
    /// Creates a new directory with the specified parameters.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to be created.</param>
    /// <param name="newDirectoryName"></param>
    /// <param name="unixFileMode">The file mode to use to create the directory. Only used on Unix based systems.</param>
    /// <param name="createParentPaths">Whether to create parent directory paths, if required, when creating the new directory.</param>
    public void CreateDirectory(string directoryPath, string newDirectoryName, UnixFileMode unixFileMode,
        bool createParentPaths)
    {
        if (createParentPaths)
        {
            if (directoryPath.EndsWith(newDirectoryName))
            {
                directoryPath = directoryPath.Remove(directoryPath.Length - newDirectoryName.Length,
                    newDirectoryName.Length);
            }

            CreateParentDirectory(directoryPath, unixFileMode);
        }
        else
        {
            if (OperatingSystem.IsWindows())
            {
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
                Directory.CreateDirectory(directoryPath, unixFileMode);
            }
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <param name="newDirectoryName"></param>
    /// <param name="createParentPaths"></param>
    public void CreateDirectory(string directoryPath, string newDirectoryName, bool createParentPaths)
    {
        if (createParentPaths)
        {
            if (directoryPath.EndsWith(newDirectoryName))
            {
                directoryPath = directoryPath.Remove(directoryPath.Length - newDirectoryName.Length, newDirectoryName.Length);
            }
            
            CreateParentDirectory(directoryPath);
        }
        else
        {
            if (OperatingSystem.IsWindows())
            {
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}