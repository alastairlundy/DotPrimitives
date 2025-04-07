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

using System;
using System.IO;

// ReSharper disable ConvertToPrimaryConstructor

namespace AlastairLundy.Resyslib.IO.Files
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