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

namespace DotPrimitives.IO.Directories;

/// <summary>
/// This interface allows for safe enumeration and retrieval of directories and files within the directory structure.
/// </summary>
public interface ISafeDirectoryProvider
{
    /// <summary>
    /// Safely enumerates directories in the current directory, ignoring
    /// inaccessible directories and handling exceptions that may occur during
    /// directory traversal.
    /// </summary>
    /// <param name="directory"></param>
    /// <returns>
    /// A sequence of <see cref="DirectoryInfo"/> objects representing the
    /// directories found in the current directory based on the default pattern "*".
    /// </returns>
    IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(DirectoryInfo directory);

    /// <summary>
    /// Safely enumerates directories in the specified directory, ignoring inaccessible
    /// directories and handling exceptions that may occur during directory traversal.
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="searchPattern">
    /// The search string to match against the names of directories. This parameter can contain
    /// a combination of valid literal path and wildcard (* and ?) characters, but it doesn't
    /// support regular expressions.
    /// </param>
    /// <param name="searchOption">
    /// Specifies whether the search operation should include all subdirectories (AllDirectories)
    /// or only the current directory (TopDirectoryOnly).
    /// </param>
    /// <param name="ignoreCase">Whether to ignore the case of the directories, true by default.</param>
    /// <returns>
    /// A sequence of <see cref="DirectoryInfo"/> objects representing the directories
    /// found in the specified directory that match the search pattern and search option.
    /// </returns>
    IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(DirectoryInfo directory, string searchPattern,
        SearchOption searchOption = SearchOption.TopDirectoryOnly,
        bool ignoreCase = false);

    /// <summary>
    /// Safely retrieves directories in the current directory, ignoring
    /// inaccessible directories and handling exceptions that may occur during
    /// the directory traversal process.
    /// </summary>
    /// <param name="directory"></param>
    /// <returns>
    /// An array of <see cref="DirectoryInfo"/> objects representing the
    /// directories found in the current directory based on the default pattern "*".
    /// </returns>
    DirectoryInfo[] SafelyGetDirectories(DirectoryInfo directory);

    /// <summary>
    /// Safely retrieves directories from the specified directory, handling exceptions
    /// and ignoring inaccessible directories during the directory traversal process.
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="searchPattern">
    /// The search string to match against directory names in the directory.
    /// </param>
    /// <param name="searchOption">
    /// Specifies whether the search operation should include only the current directory
    /// or all subdirectories. Use <see cref="SearchOption.TopDirectoryOnly"/> to include
    /// only the current directory, or <see cref="SearchOption.AllDirectories"/> to include
    /// all subdirectories.
    /// </param>
    /// <param name="ignoreCase">
    /// A boolean value indicating whether the search pattern matching should ignore case sensitivity.
    /// </param>
    /// <returns>
    /// An array of <see cref="DirectoryInfo"/> objects representing the directories found
    /// based on the specified search parameters.
    /// </returns>
    DirectoryInfo[] SafelyGetDirectories(DirectoryInfo directory, string searchPattern,
        SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = false);

    /// <summary>
    /// Safely enumerates files in the specified directory, handling inaccessible or special directories gracefully.
    /// </summary>
    /// <param name="directory">The directory to enumerate files from.</param>
    /// <returns>Returns an enumerable collection of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
    IEnumerable<FileInfo> SafelyEnumerateFiles(DirectoryInfo directory);

    /// <summary>
    /// Safely enumerates files in the specified directory.
    /// </summary>
    /// <param name="directory">The directory to enumerate files from.</param>
    /// <param name="searchPattern">The search string to match against the names of files in the directory.</param>
    /// <param name="searchOption">Specifies whether to search only the current directory or all subdirectories.</param>
    /// <param name="ignoreCase">Specifies whether the search pattern should be case-insensitive.</param>
    /// <returns>Returns a sequence of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
    IEnumerable<FileInfo> SafelyEnumerateFiles(DirectoryInfo directory,
        string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly,
        bool ignoreCase = false);

    /// <summary>
    /// Safely retrieves an array of files in the specified directory, using a default
    /// search pattern of "*", while handling inaccessible or locked files gracefully.
    /// </summary>
    /// <param name="directory"></param>
    /// <returns>Returns an array of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
    FileInfo[] SafelyGetFiles(DirectoryInfo directory);

    /// <summary>
    /// Safely retrieves an array of files from the specified directory, using the provided
    /// search pattern, search option, and case sensitivity, while handling inaccessible or locked files gracefully.
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="searchPattern">The search pattern to match against the file names in the directory.</param>
    /// <param name="searchOption">Specifies whether to search only the current directory or all subdirectories.</param>
    /// <param name="ignoreCase">Indicates whether the search pattern will be treated as case-insensitive.</param>
    /// <returns>Returns an array of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
    FileInfo[] SafelyGetFiles(DirectoryInfo directory, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly,
        bool ignoreCase = false);
}