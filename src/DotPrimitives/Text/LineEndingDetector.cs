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

namespace DotPrimitives.Text;

/// <summary>
/// A static class to detect line endings in a string or in a file.
/// </summary>
public static class LineEndingDetector
{
    /// <param name="source">The string to be checked.</param>
    extension(string source)
    {
        /// <summary>
        /// Gets the line ending of a string.
        /// </summary>
        /// <returns>the line ending format of the string.</returns>
        public LineEndingFormat GetLineEndingFormat()
        {
            char lastChar = source.Last();

            bool containsR = source.IndexOf('\r') != -1;
            bool containsN = source.IndexOf('\n') != -1;

            return lastChar switch
            {
                '\n' => containsR ? LineEndingFormat.CR_LF : LineEndingFormat.LF,
                '\r' => containsN ? LineEndingFormat.LF_CR : LineEndingFormat.CR,
                _ => LineEndingFormat.NotDetected
            };
        }
    }
}