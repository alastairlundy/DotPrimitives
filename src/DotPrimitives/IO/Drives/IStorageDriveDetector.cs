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
/// Represents a contract for detecting and enumerating system storage drives.
/// </summary>
public interface IStorageDriveDetector
{
    /// <summary>
    /// Enumerates all physical internal drives available on the system.
    /// </summary>
    /// <remarks>Does not enumerate external drives or network drives.</remarks>
    /// <returns>
    /// An enumerable collection of <see cref="System.IO.DriveInfo"/> objects representing the physical drives
    /// available on the system. Throws a <see cref="PlatformNotSupportedException"/> if the platform is not supported.
    /// </returns>
    IEnumerable<DriveInfo> EnumeratePhysicalDrives();

    /// <summary>
    /// Enumerates all logical drives available on the current platform.
    /// </summary>
    /// <returns>
    /// A sequence of <see cref="System.IO.DriveInfo"/> objects representing the logical drives
    /// accessible on the system.
    /// </returns>
    IEnumerable<DriveInfo> EnumerateLogicalDrives();

    /// <summary>
    /// Retrieves an array of all physical drives available on the system.
    /// </summary>
    /// <returns>
    /// An array of <see cref="System.IO.DriveInfo"/> objects representing the physical drives
    /// available and ready for access on the system. Returns an empty array if no drives are found.
    /// </returns>
    DriveInfo[] GetPhysicalDrives();

    /// <summary>
    /// Retrieves an array of all logical drives available on the system.
    /// </summary>
    /// <returns>
    /// An array of <see cref="System.IO.DriveInfo"/> objects representing the logical drives
    /// available on the system. Returns an empty array if no logical drives are found.
    /// </returns>
    DriveInfo[] GetLogicalDrives();
}