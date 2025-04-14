/*
    Resyslib
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System.IO;
// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable RedundantBoolCompare

namespace AlastairLundy.Resyslib.Text
{
    /// <summary>
    /// A static class to detect line endings in a string or in a file.
    /// </summary>
    public static class LineEndingDetector
    {
        /// <summary>
        /// Gets the line ending of a file.
        /// </summary>
        /// <param name="filePath">The file path of the file to be checked.</param>
        /// <returns>the line ending format of the string.</returns>
        public static LineEndingFormat GetLineEndingInFile(this string filePath)
        {
            try
            {
                string[] contents = File.ReadAllLines(filePath);
            
                return GetLineEnding(contents[0]);
            }
            catch
            {
                return LineEndingFormat.NotDetected;
            }
        }
    
        
        /// <summary>
        /// Gets the line ending of a string.
        /// </summary>
        /// <param name="source">The string to be checked.</param>
        /// <returns>the line ending format of the string.</returns>
        public static LineEndingFormat GetLineEnding(this string source)
        {
            LineEndingFormat lineEndingFormat;
        
            if (source.EndsWith('\n') && source.Contains('\r') == true)
            {
                lineEndingFormat = LineEndingFormat.LF_CR;
            }
            else if (source.EndsWith('\r') && source.Contains('\n') == true)
            {
                lineEndingFormat = LineEndingFormat.CR_LF;
            }
            else if (source.EndsWith('\n') && source.Contains('\r') == false)
            {
                lineEndingFormat = LineEndingFormat.LF;
            }
            else if (source.EndsWith('\r') && source.Contains('\n') == false)
            {
                lineEndingFormat = LineEndingFormat.CR;
            }
            else
            {
                lineEndingFormat = LineEndingFormat.NotDetected;
            }

            return lineEndingFormat;
        }
    }
}