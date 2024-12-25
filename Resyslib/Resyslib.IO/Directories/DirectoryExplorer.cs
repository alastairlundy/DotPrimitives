/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.IO;
using System.Linq;

using Resyslib.Internal.Localizations;
using Resyslib.IO.Directories.Extensions;

// ReSharper disable RedundantBoolCompare

namespace Resyslib.IO.Directories;

/// <summary>
/// 
/// </summary>
public class DirectoryExplorer : IRecursiveDirectoryExplorer
{
    protected DirectoryCreator DirectoryCreator;

    public DirectoryExplorer()
    {
        DirectoryCreator = new DirectoryCreator();
    }
    
    /// <summary>
    /// Determines whether subdirectories of a directory are empty.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <returns>true if all subdirectories in a directory are empty; returns false otherwise.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist or could not be located.</exception>
    public bool AreSubdirectoriesEmpty(string directory)
    {
        if (!Directory.Exists(directory))
        {
            throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
        }
        
        string[] subDirectories = Directory.GetDirectories(directory);
                
        bool[] allowRecursiveEmptyDirectoryDeletion = new bool[subDirectories.Length];

        for (int i = 0; i < subDirectories.Length; i++)
        {
            string dir = subDirectories[i];
                    
            if (DirectoryHelper.IsDirectoryEmpty(dir))
            {
                allowRecursiveEmptyDirectoryDeletion[i] = true;
            }
            else
            {
                allowRecursiveEmptyDirectoryDeletion[i] = false;
            }
        }

        return allowRecursiveEmptyDirectoryDeletion.All(x => x == true);
    }
    
    /// <summary>
    /// Gets the directories and files within a parent directory.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <returns>the directories and files within a parent directory.</returns>
    public (IEnumerable<string> files, IEnumerable<string> directories) GetRecursiveDirectoryContents(string directory)
    {
        var output = GetRecursiveDirectoryContents(directory, true);
        return (output.files, output.directories);
    }

    /// <summary>
    /// Gets the directories and files within a parent directory.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <param name="includeEmptyDirectories">Whether to include empty directories or not.</param>
    /// <returns>the directories and files within a parent directory.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist.</exception>
    public (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories)
        GetRecursiveDirectoryContents(string directory,
            bool includeEmptyDirectories)
    {
        List<string> files = new List<string>();
        List<string> directories = new List<string>();
        List<string> emptyDirectories = new List<string>();
        
        if (Directory.Exists(directory))
        {
            if (Directory.GetDirectories(directory).Length > 0)
            {
                foreach (string subDirectory in Directory.GetDirectories(directory))
                {
                    if (Directory.GetFiles(subDirectory).Length > 0)
                    {
                        foreach (string file in Directory.GetFiles(subDirectory))
                        {
                            files.Add(file);
                        }
                    }

                    int numberOfFiles = Directory.GetFiles(subDirectory).Length;

                    if (numberOfFiles > 0)
                    {
                        directories.Add(subDirectory);
                    }
                    else if (includeEmptyDirectories == true && directory.IsDirectoryEmpty())
                    {
                        emptyDirectories.Add(subDirectory);
                    }
                }
            }
            else
            {
                if (includeEmptyDirectories)
                {
                    emptyDirectories.Add(directory);
                }
            }

            return (files, directories, emptyDirectories);
        }

        throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public IEnumerable<string> GetRecursiveEmptyDirectories(string directory)
    {
        return GetRecursiveDirectoryContents(directory, true).emptyDirectories;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public IEnumerable<string> GetFilesRecursively(string directory)
    {
        return GetRecursiveDirectoryContents(directory, false).files;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public IEnumerable<string> GetFolderRecursively(string directory)
    {
        return GetRecursiveDirectoryContents(directory, false).directories;
    }
}