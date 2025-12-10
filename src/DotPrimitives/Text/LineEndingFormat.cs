/*
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

// ReSharper disable InconsistentNaming
namespace AlastairLundy.DotPrimitives.Text;

/// <summary>
/// Specifies the format of line endings used in text content.
/// </summary>
public enum LineEndingFormat
{
    /// <summary>
    /// Represents a line-ending format that uses a single carriage return ('\r') character.
    /// Commonly used in legacy macOS systems.
    /// </summary>
    CR,
    /// <summary>
    /// Represents a line-ending format that uses a single line feed ('\n') character.
    /// Commonly used in Unix-based systems, including Linux and modern macOS.
    /// </summary>
    LF,
    /// <summary>
    /// Represents a line-ending format that uses a combination of a carriage return ('\r') character
    /// followed by a line feed ('\n') character.
    /// Commonly used in Windows operating systems.
    /// </summary>
    CR_LF,
    /// <summary>
    /// Represents a line-ending format that uses a line feed ('\n') character
    /// followed by a carriage return ('\r') character.
    /// </summary>
    LF_CR,
    /// <summary>
    /// Represents a state where the line-ending format was not detected.
    /// This may occur when the text content lacks recognizable line-ending sequences
    /// or is in an unsupported format.
    /// </summary>
    NotDetected
}