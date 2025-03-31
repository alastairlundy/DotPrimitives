

using System;
using System.IO;

// ReSharper disable ConvertToPrimaryConstructor

namespace Resyslib.IO.Files
{
    /// <summary>
    /// 
    /// </summary>
    public class FileModel : IEquatable<FileModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// 
        /// </summary>
        public string FileExtension { get; }
    
        /// <summary>
        /// 
        /// </summary>
        public string FilePath { get; }
    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public FileModel(string filePath)
        {
            FileExtension = Path.HasExtension(filePath) ? Path.GetExtension(filePath) : string.Empty;

            FileName = Path.GetFileNameWithoutExtension(filePath);
        
            FilePath = Path.GetFullPath(filePath);
        }

    
        public override string ToString()
        {
            return FilePath;
        }

        public bool Equals(FileModel? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return FileName == other.FileName && FileExtension == other.FileExtension && FilePath == other.FilePath;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj is FileModel model)
            {
                return Equals(model);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(FileName, FileExtension, FilePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(FileModel? left, FileModel? right)
        {
            if (left is null || right is null)
            {
                return false;
            }
            
            return left.Equals(right);
        }
        
        public static bool operator ==(FileModel? left, FileModel? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FileModel? left, FileModel? right)
        {
            return Equals(left, right) == false;
        }
    }
}