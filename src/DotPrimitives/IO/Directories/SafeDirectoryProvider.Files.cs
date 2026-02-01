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

internal partial class SafeDirectoryProvider
{
    /// <summary>
    /// Safely enumerates files in the specified directory, handling inaccessible or special directories
    /// based on the provided search pattern, search option, and case sensitivity preference.
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="searchPattern">The search string to match against the names of files in the directory.</param>
    /// <param name="searchOption">Specifies whether to search only the current directory or all subdirectories.</param>
    /// <param name="ignoreCase">Specifies whether the search pattern should be case-insensitive.</param>
    /// <returns>Returns a sequence of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
    internal IEnumerable<FileInfo> SafelyEnumerateFiles(DirectoryInfo directory,
        string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly,
        bool ignoreCase = false)
    {
#if NET8_0_OR_GREATER
        return SafeFileEnumeration_Net8Plus(directory, searchPattern, searchOption, ignoreCase);
#else
        return SafeFileEnumeration_NetStandard20(directory, searchPattern, searchOption, ignoreCase);
#endif
    }

    #region Implementation Methods

#if NETSTANDARD2_1 || NET8_0_OR_GREATER
    private IEnumerable<FileInfo> SafeFileEnumeration_Net8Plus(DirectoryInfo directory, string searchPattern, SearchOption searchOption,
        bool ignoreCase)
    {
        EnumerationOptions enumerationOptions = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = searchOption == SearchOption.AllDirectories,
            ReturnSpecialDirectories = true,
            MatchType = MatchType.Simple,
            MatchCasing = ignoreCase ? MatchCasing.CaseInsensitive : MatchCasing.CaseSensitive
        };

        return directory.EnumerateFiles(searchPattern, enumerationOptions);
    }

    private FileInfo[] SafeFileGetting_Net8Plus(DirectoryInfo directory, string searchPattern, SearchOption searchOption, 
        bool ignoreCase)
    {
        EnumerationOptions enumerationOptions = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = searchOption == SearchOption.AllDirectories,
            ReturnSpecialDirectories = true,
            MatchType = MatchType.Simple,
            MatchCasing = ignoreCase ? MatchCasing.CaseInsensitive : MatchCasing.CaseSensitive
        };

        return directory.GetFiles(searchPattern, enumerationOptions);
    }
#else
    /// <summary>
    /// This method is internal for Benchmarking and Unit Testing purposes only.
    /// </summary>
    internal IEnumerable<FileInfo> SafeFileEnumeration_NetStandard20(DirectoryInfo directory, string searchPattern,
        SearchOption searchOption, bool ignoreCase)
    {
        if (!directory.Exists)
            throw new DirectoryNotFoundException();

        IEnumerable<FileInfo> files = directory.EnumerateFiles(searchPattern, searchOption)
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

        foreach (FileInfo fileInfo in files)
        {
            StringComparison stringComparison =
                ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            if (searchPattern != "*" && searchPattern != "?")
            {
                bool result = fileInfo.Name.Contains(searchPattern, stringComparison);

                if (result)
                    yield return fileInfo;
            }
            else if (searchPattern.Contains("*") || searchPattern.Contains("?"))
            {
                yield return fileInfo;
            }
        }
    }
#endif
    #endregion
}