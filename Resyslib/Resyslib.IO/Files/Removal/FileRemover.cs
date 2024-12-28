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
using System.Linq;

using Resyslib.IO.Internal.Localizations;

// ReSharper disable ConditionalAccessQualifierIsNonNullableAccordingToAPIContract

namespace Resyslib.IO.Files;

public class FileRemover : IFileRemover
{
    public event EventHandler<string> FileDeleted;

    public FileRemover()
    {
        
    }

    /// <summary>
    /// Attempts to delete a file.
    /// </summary>
    /// <param name="file">The file to be deleted.</param>
    /// <returns>true if the file has been successfully deleted; returns false otherwise.</returns>
    public bool TryDeleteFile(string file)
    {
        try
        {
            DeleteFile(file);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Deletes a file.
    /// </summary>
    /// <param name="file">The file to be deleted.</param>
    /// <exception cref="FileNotFoundException">Thrown if the file doesn't exist.</exception>
    public void DeleteFile(string file)
    {
        if (File.Exists(file))
        {
            File.Delete(file);
            FileDeleted?.Invoke(this, Resources.IO_File_Deleted.Replace("{x}", file));
        }

        throw new FileNotFoundException(Resources.Exceptions_IO_FileNotFound.Replace("{x}", file));
    }

    /// <summary>
    /// Deletes multiple files.
    /// </summary>
    /// <param name="files">The files to be deleted.</param>
    public void DeleteFiles(IEnumerable<string> files)
    {
        string[] enumerable = files as string[] ?? files.ToArray();
        for(int i = 0; i < enumerable.Count(); i++)
        {
            DeleteFile(enumerable[i]);
        }
    }
}