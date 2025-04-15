/*
    Resyslib.IO
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.IO;
// ReSharper disable MemberCanBePrivate.Global

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
        /// A model to represent a File.
        /// </summary>
        /// <param name="filePath">The file path of a file to represent as a FileModel.</param>
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
            if (other is null)
            {
                return false;
            }
            
            return FileName.Equals(other.FileName) &&
                   FileExtension.Equals(other.FileExtension) &&
                   FilePath.Equals(other.FilePath);
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
        /// Returns a hash code value for this object.
        /// </summary>
        /// <returns>A hash code value for this object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(FileName, FileExtension, FilePath);
        }

        /// <summary>
        /// Determines whether two file models are equal.
        /// </summary>
        /// <param name="left">The FileModel instance to compare with the current object.</param>
        /// <param name="right">The other FileModel instance to compare with the current object.</param>
        /// <returns>True if both file models are equal; false othewise.</returns>
        public static bool Equals(FileModel? left, FileModel? right)
        {
            if (left is null || right is null)
            {
                return false;
            }
            
            return left.Equals(right);
        }
        
        /// <summary>
        /// Determines whether two File Models are equal to each other.
        /// </summary>
        /// <param name="left">The first file model to compare.</param>
        /// <param name="right">The second file model to compare.</param>
        /// <returns>True if both file models are equal; false otherwise.</returns>
        public static bool operator ==(FileModel? left, FileModel? right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two File Models are not equal to each other.
        /// </summary>
        /// <param name="left">The first file model to compare.</param>
        /// <param name="right">The file model to compare.</param>
        /// <returns> True if the file models are not equal; false otherwise.</returns>
        public static bool operator !=(FileModel? left, FileModel? right)
        {
            return Equals(left, right) == false;
        }
    }
}