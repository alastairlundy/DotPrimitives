/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.IO;

namespace Resyslib.IO.Files;

public class FileFinder : IFileFinder
{
    /// <summary>
    /// Determines whether a string is the name of a file.
    /// </summary>
    /// <param name="filePath">The string to be searched.</param>
    /// <returns>true if the string is a file; return false otherwise.</returns>
    public bool IsAFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                return true;
            }
            
            if (filePath.Length > 1)
            {
                if (filePath.Length - 4 >= 0 && filePath.Length - 4 < filePath.Length)
                {
                    // Uses new .NET 6 and newer ^ Index
                    if (filePath[^4].Equals('.'))
                    {
                        return true;
                    }
                }
                if (filePath.Length - 3 >= 0 && filePath.Length - 3 < filePath.Length)
                {
                    // Uses new .NET 6 and newer ^ Index
                    if (filePath[^3].Equals('.') || filePath[^2].Equals('.'))
                    {
                        return true;
                    }
                }

                if (filePath.Length - 2 >= 0 && filePath.Length - 2 < filePath.Length)
                {
                    // Uses new .NET 6 and newer ^ Index
                    if (filePath[^2].Equals('.'))
                    {
                        return true;
                    }
                }
            }
            
            return File.Exists(filePath);
        }
        catch
        {
            return false;
        }
    }
}