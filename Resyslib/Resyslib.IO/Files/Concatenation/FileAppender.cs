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
using System.Text;
using System.Threading.Tasks;
using Resyslib.Internal.Localizations;

// ReSharper disable RedundantIfElseBlock

namespace Resyslib.IO.Files;

/// <summary>
/// A class to append the contents of files.
/// </summary>
public class FileAppender : IFileAppender
{
    private readonly StringBuilder _appendedFileContents;
        
    private readonly IFileFinder _fileFinder;
    
    public FileAppender()
    {
        _appendedFileContents = new StringBuilder();
        _fileFinder = new FileFinder();
    }

    public FileAppender(IFileFinder fileFinder)
    {
        _fileFinder = fileFinder;
        _appendedFileContents = new StringBuilder();
    }

    public async Task AppendFileAsync(FileModel fileToBeAppended)
    {
        if (_fileFinder.IsAFile(fileToBeAppended.FilePath) || File.Exists(fileToBeAppended.FilePath))
        {
            try
            {
                string[] lines = await File.ReadAllLinesAsync(fileToBeAppended.FilePath);

                foreach (var line in lines)
                {
                    _appendedFileContents.AppendLine(line);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
        else
        {
            throw new FileNotFoundException(Resources.Exceptions_FileNotFound, fileToBeAppended);
        }
    }

    /// <summary>
    /// Attempts to append the contents of a file to an existing list.
    /// </summary>
    /// <param name="fileToBeAppended">The file to have its contents appended to the existing file contents. If no existing file contents exists, this will become the contents appended to in the future.</param>
    /// <returns>true if the file was successfully appended; returns false otherwise.</returns>
    public bool TryAppendFile(string fileToBeAppended)
    {
        try
        {
            AppendFile(fileToBeAppended);

            return true;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Appends the contents of a file to an existing list.
    /// </summary>
    /// <param name="fileToBeAppended">The file to be appended.</param>
    /// <exception cref="Exception">Thrown if the file appending fails.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
    public void AppendFile(FileModel fileToBeAppended)
    {
        if (_fileFinder.IsAFile(fileToBeAppended.FilePath) || File.Exists(fileToBeAppended.FilePath))
        {
            try
            {
                string[] lines = File.ReadAllLines(fileToBeAppended.FilePath);

                foreach (var line in lines)
                {
                    _appendedFileContents.AppendLine(line);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
        else
        {
            throw new FileNotFoundException(Resources.Exceptions_FileNotFound, fileToBeAppended);
        }
    }

    public async Task AppendFileAsync(string fileToBeAppended)
    {
        if (_fileFinder.IsAFile(fileToBeAppended) || File.Exists(fileToBeAppended))
        {
            try
            {
                string[] lines = await File.ReadAllLinesAsync(fileToBeAppended);

                foreach (string line in lines)
                {
                    _appendedFileContents.AppendLine(line);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
        else
        {
            throw new FileNotFoundException(Resources.Exceptions_FileNotFound, fileToBeAppended);
        }
    }

    /// <summary>
    /// Appends the contents of a file to an existing list.
    /// </summary>
    /// <param name="fileToBeAppended">The file to be appended.</param>
    /// <exception cref="Exception">Thrown if the file appending fails.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
    public void AppendFile(string fileToBeAppended)
    {
        if (_fileFinder.IsAFile(fileToBeAppended) || File.Exists(fileToBeAppended))
        {
            try
            {
                string[] lines = File.ReadAllLines(fileToBeAppended);

                foreach (string line in lines)
                {
                    _appendedFileContents.AppendLine(line);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
        else
        {
            throw new FileNotFoundException(Resources.Exceptions_FileNotFound, fileToBeAppended);
        }
    }

    /// <summary>
    /// Append the contents of an ordered enumerable of files to an existing list.
    /// </summary>
    /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
    public void AppendFiles(IOrderedEnumerable<string> filesToBeAppended)
    {
        AppendFiles(filesToBeAppended.ToArray());
    }

    /// <summary>
    /// Append the contents of the IEnumerable of files to an existing list.
    /// </summary>
    /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
    public void AppendFiles(IEnumerable<string> filesToBeAppended)
    {
        foreach (string file in filesToBeAppended)
        {
            AppendFile(file);
        }
    }
    
    /// <summary>
    /// Append the contents of the IEnumerable of files to an existing list.
    /// </summary>
    /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
    public void AppendFiles(IEnumerable<FileModel> filesToBeAppended)
    {
        foreach (FileModel file in filesToBeAppended)
        {
            AppendFile(file);
        }
    }

    /// <summary>
    /// Attempts to append the contents of files to an existing list.
    /// </summary>
    /// <param name="filesToBeAppended">The files to be appended to the existing file contents. If no existing file contents exists, this will become the contents appended to in the future.</param>
    /// <returns>true if the files were successfully appended; returns false otherwise.</returns>
    public bool TryAppendFiles(IEnumerable<string> filesToBeAppended)
    {
        try
        {
            AppendFiles(filesToBeAppended);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Returns the appended contents as an IEnumerable.
    /// </summary>
    /// <returns>the list of appended strings as an enumerable.</returns>
    public IEnumerable<string> ToEnumerable()
    {
        return _appendedFileContents.ToString().Split(Environment.NewLine);
    }

    public override string ToString()
    {
        return _appendedFileContents.ToString();
    }

    /// <summary>
    /// Writes the appended strings to a file.
    /// </summary>
    /// <param name="file">The file to be saved to.</param>
    /// <exception cref="ArgumentException">Thrown if an invalid file path is provided.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
    /// <exception cref="Exception">Thrown if an exception occurs when attempting to write the file.</exception>
    public void WriteToFile(FileModel file)
    {
        WriteToFile(file.FilePath);
    }

    /// <summary>
    /// Writes the appended strings to a file.
    /// </summary>
    /// <param name="filePath">The path to save the file to.</param>
    /// <exception cref="ArgumentException">Thrown if an invalid file path is provided.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
    /// <exception cref="Exception">Thrown if an exception occurs when attempting to write the file.</exception>
    public void WriteToFile(string filePath)
    {
        if (_fileFinder.IsAFile(filePath))
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                File.WriteAllLines(filePath, ToEnumerable());
            }
            catch (FileNotFoundException exception)
            {
                throw new FileNotFoundException(exception.Message, filePath, exception);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
        else
        {
            throw new ArgumentException(Resources.Exceptions_NoFileProvided, filePath);
        }
    }
}