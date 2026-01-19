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

namespace DotPrimitives.Collections.Enumerables.Cached;

/// <summary>
/// Defines an interface for an Enumerable that can be cached and materialized on demand,
/// with the ability to refresh the cache.
/// </summary>
/// <typeparam name="T">The type of elements in the enumeration.</typeparam>
public interface IRefreshableCachedEnumerable<T> : ICachedEnumerable<T>
{
    /// <summary>
    /// Requests a refresh of the internal cache by repopulating it from the given source data.
    /// Developers should use this method when the underlying data has changed or been updated.
    /// </summary>
    /// <param name="source">The new source data to use for repopulating the cache.</param>
    void RefreshCache(IEnumerable<T> source);
}