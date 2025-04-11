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
    /// A model to represent a File.
    /// </summary>
    public class FileModel : IEquatable<FileModel>
    {
        /// <summary>
        /// The name of the file being represented.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// The file's extension.
        /// </summary>
        public string FileExtension { get; }
    
        /// <summary>
        /// The path to the file.
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

    
        /// <summary>
        /// Returns the file path of the file.
        /// </summary>
        /// <returns>The file path of the file.</returns>
        public override string ToString()
        {
            return FilePath;
        }

        /// <summary>
        /// Returns whether a file model is equal to another file model.
        /// </summary>
        /// <param name="other">The other file model to compare to this one.</param>
        /// <returns>True if both file models are the same; false otherwise.</returns>
        public bool Equals(FileModel? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return FileName == other.FileName && FileExtension == other.FileExtension && FilePath == other.FilePath;
        }

        /// <summary>
        /// Returns whether a file model is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare to this file model.</param>
        /// <returns>True if the file model is a FileModel and is equal to this file model; false otherwise.</returns>
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