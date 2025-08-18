﻿/*
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

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantBoolCompare

namespace AlastairLundy.DotPrimitives.Text;

/// <summary>
/// A static class to detect line endings in a string or in a file.
/// </summary>
public static class LineEndingDetector
{
    
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