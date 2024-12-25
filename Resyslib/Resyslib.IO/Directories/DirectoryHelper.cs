using System.IO;
using Resyslib.Internal.Localizations;

namespace Resyslib.IO.Directories;

public static class DirectoryHelper
{
    
    
    /// <summary>
    /// Checks if a Directory is empty or not.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <returns>true if the directory is empty; returns false otherwise.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist.</exception>
    public static bool IsDirectoryEmpty(string directory)
    {
        if (Directory.Exists(directory))
        {
            return Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0;
        }
        else
        {
            throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
        }
    }
}