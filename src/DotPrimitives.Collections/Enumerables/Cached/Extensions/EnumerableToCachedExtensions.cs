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

using System.Collections.Generic;

namespace AlastairLundy.DotPrimitives.Collections.Enumerables.Cached;

/// <summary>
/// 
/// </summary>
public static class EnumerableToCachedExtensions
{
   /// <summary>
   /// 
   /// </summary>
   /// <param name="source">The underlying enumerable data to be cached.</param>
   /// <param name="mode">The desired level of materialization for the cached values,
   /// defaults to Lazy if not provided.
   /// </param>
   /// <typeparam name="T">The type of elements stored in the CachedEnumerable.</typeparam>
   /// <returns>An instantiated <see cref="CachedEnumerable{T}"/> with the specified source <see cref="IEnumerable{T}"/> and <see cref="EnumerableMaterializationMode"/>.</returns>
   public static CachedEnumerable<T> Cache<T>(this IEnumerable<T> source,
      EnumerableMaterializationMode mode = EnumerableMaterializationMode.Lazy)
   {
      return new CachedEnumerable<T>(source, mode);
   }

   /// <summary>
   /// 
   /// </summary>
   /// <param name="source">The underlying enumerable data to be cached.</param>
   /// <param name="mode">The desired level of materialization for the cached values,
   /// defaults to Lazy if not provided.
   /// </param>
   /// <typeparam name="T">The type of elements stored in the RefreshableCachedEnumerable.</typeparam>
   /// <returns>An instantiated <see cref="RefreshableCachedEnumerable{T}"/> with the specified source <see cref="IEnumerable{T}"/> and <see cref="EnumerableMaterializationMode"/>.</returns>
   public static RefreshableCachedEnumerable<T> CacheAsRefreshable<T>(this IEnumerable<T> source,
      EnumerableMaterializationMode mode = EnumerableMaterializationMode.Lazy)
   {
      return new RefreshableCachedEnumerable<T>(source, mode);
   }
}