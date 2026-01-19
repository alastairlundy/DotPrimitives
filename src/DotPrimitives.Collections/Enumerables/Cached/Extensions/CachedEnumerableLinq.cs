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

// ReSharper disable RedundantBoolCompare
namespace DotPrimitives.Collections.Enumerables.Cached;

/// <summary>
/// A class to hold extension methods for Cached Enumerable types.
/// </summary>
public static class CachedEnumerableLinq
{
    /// <param name="cachedEnumerable">The cached enumerable to check.</param>
    /// <typeparam name="T">The type of elements stored in the CachedEnumerable.</typeparam>
    extension<T>(ICachedEnumerable<T> cachedEnumerable)
    {
        /// <summary>
        /// Determines if any items are in the Cached Enumerable's source.
        /// </summary>
        /// <remarks>This first attempts to check for any elements without materializing the source enumerable.
        /// <para>If this check is unsuccessful in gauging whether any elements are in the source, the cache is accessed, thereby forcing materialization of the Cache.</para>
        /// <para></para>
        /// <para>If potential premature materialization of the Cache is undesirable,
        /// use <see cref="ICachedEnumerable{T}"/>'s IsEmpty property.</para>
        /// </remarks>
        /// <returns>True if any elements are in the source enumerable, false otherwise.</returns>
        public bool Any()
        {
            if (cachedEnumerable.HasBeenMaterialized)
                return cachedEnumerable.IsEmpty;
        
            if (cachedEnumerable.IsEmpty == false)
                return true;

            foreach (T item in cachedEnumerable)
            {
                return true;
            }

            return false;
        }
    }
}