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

namespace DotPrimitives.IO.Drives;

/// <summary>
/// Provides functionality for detecting and interacting with storage drives on various platforms.
/// </summary>
/// <remarks>
/// This class offers a shared instance of <see cref="IStorageDriveDetector"/> used to enumerate
/// physical and logical drives. It supports platform-specific implementations for drive detection.
/// </remarks>
public static class StorageDrives
{
    /// <summary>
    /// A shared instance of an <see cref="IStorageDriveDetector"/> implementation.
    /// </summary>
    /// <remarks>
    /// This property provides access to a singleton instance of <see cref="StorageDriveDetector"/>,
    /// which can be used to enumerate and retrieve information about physical and logical storage drives
    /// on the system. The instance is readonly and is shared across the application.
    /// </remarks>
    public static IStorageDriveDetector Shared { get; } = new StorageDriveDetector();
}