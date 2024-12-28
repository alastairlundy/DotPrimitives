/*
    Resyslib.IO 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace Resyslib.IO.Files
{
    /// <summary>
    /// Syntactic sugar around the FileAppender class.
    /// </summary>
    public static class FileConcatenator
    {
        /// <summary>
        /// Concatenated the contents of files in the style of the Unix Cat command.
        /// </summary>
        /// <param name="files">The files to be concatenated.</param>
        /// <returns>the concatenated files as an IEnumerable of strings.</returns>
        public static IEnumerable<string> ConcatenateFilesToEnumerable(IEnumerable<string> files)
        {
            FileAppender fileAppender = new FileAppender();
            fileAppender.AppendFiles(files);

            return fileAppender.ToEnumerable();
        }
        
        /// <summary>
        /// Concatenates the contents of specified files and saves it to a new file.
        /// </summary>
        /// <param name="filePath">The path to save the new file to.</param>
        /// <param name="newFileName">The name of the new file to be created.</param>
        /// <param name="files">The files to be concatenated.</param>
        /// <exception cref="Exception">Thrown if an exception occurs when trying to save the file.</exception>
        public static void ConcatenateFilesToNewFile(string filePath, string newFileName, IEnumerable<string> files)
        {
            try
            {
                string newFile = $"{filePath}{Path.DirectorySeparatorChar}{newFileName}";

                if (filePath.Contains(newFileName) == false)
                {
                    File.WriteAllLines(newFile, ConcatenateFilesToEnumerable(files));
                }
                else
                {
                    File.WriteAllLines(newFileName, ConcatenateFilesToEnumerable(files));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}