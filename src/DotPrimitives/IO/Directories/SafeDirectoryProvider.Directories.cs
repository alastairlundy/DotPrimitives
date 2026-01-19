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
/// Provides methods for safely interacting with directories by handling exceptions
/// and avoiding inaccessible file system elements during directory traversal.
/// </summary>
public partial class SafeDirectoryProvider : ISafeDirectoryProvider
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
    public IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(DirectoryInfo directory)
        => SafelyEnumerateDirectories(directory, "*");

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
    public IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(DirectoryInfo directory, string searchPattern,
        SearchOption searchOption = SearchOption.TopDirectoryOnly,
        bool ignoreCase = false)
    {
#if NETSTANDARD2_1 || NET8_0_OR_GREATER
        return SafeDirectoryEnumeration_Net8Plus(directory, searchPattern, searchOption, ignoreCase);
#else
        return SafeDirectoryEnumeration_NetStandard20(directory, searchPattern, searchOption, ignoreCase);
#endif
    }
    
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
    public DirectoryInfo[] SafelyGetDirectories(DirectoryInfo directory)
        => SafelyGetDirectories(directory, "*");

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
    public DirectoryInfo[] SafelyGetDirectories(DirectoryInfo directory, string searchPattern,
        SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = false)
        => SafelyEnumerateDirectories(directory, searchPattern, searchOption, ignoreCase).ToArray();

    #region Implementation Methods
#if NET8_0_OR_GREATER || NETSTANDARD2_1
    private IEnumerable<DirectoryInfo> SafeDirectoryEnumeration_Net8Plus(DirectoryInfo directory, string searchPattern,
        SearchOption searchOption, bool ignoreCase)
    {
        EnumerationOptions enumerationOptions = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = searchOption == SearchOption.AllDirectories,
            ReturnSpecialDirectories = true,
            MatchType = MatchType.Simple,
            MatchCasing = ignoreCase ? MatchCasing.CaseSensitive : MatchCasing.CaseInsensitive,
            MaxRecursionDepth = searchOption == SearchOption.AllDirectories ? int.MaxValue : 0
        };

        return directory.EnumerateDirectories(searchPattern, enumerationOptions);
    }
#else
    /// <summary>
    /// This method is internal for Benchmarking and Unit Testing purposes only.
    /// </summary>
    internal IEnumerable<DirectoryInfo> SafeDirectoryEnumeration_NetStandard20(DirectoryInfo directory, string searchPattern,
        SearchOption searchOption,
        bool ignoreCase)
    {
        if (!directory.Exists)
            throw new DirectoryNotFoundException();

        IEnumerable<DirectoryInfo> directories = directory.
            EnumerateDirectories(searchPattern, searchOption)
            .Where(f =>
            {
                try
                {
                    return f.Exists;
                }
                catch
                {
                    return false;
                }
            });

        foreach (DirectoryInfo subDirectory in directories)
        {
            if (searchPattern != "*" && searchPattern != "?")
            {
                if (!subDirectory.Exists)
                    continue;
                        
                string baseDirectory = Path.GetDirectoryName(subDirectory.FullName) ?? subDirectory.Name;

                StringComparison comparison = ignoreCase
                    ? StringComparison.InvariantCultureIgnoreCase
                    : StringComparison.InvariantCulture;

                bool result = subDirectory.FullName.StartsWith(baseDirectory, comparison) ||
                              subDirectory.FullName.Contains(baseDirectory, comparison);

                if (result)
                    yield return subDirectory;
            }
            else if(searchPattern.Contains('*') || searchPattern.Contains('?'))
            {
                yield return subDirectory;
            }
        }
    }
#endif
    #endregion
}