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
using System.Collections;
using System.Collections.Generic;

// ReSharper disable RedundantEmptySwitchSection

namespace AlastairLundy.DotPrimitives.Collections.Enumerables.Cached;

/// <summary>
/// Implements the IRefreshableCachedEnumerable interface,
/// providing a way to refresh the cached data and retrieve its values.
/// </summary>
/// <typeparam name="T">The type of elements in the Enumerable.</typeparam>
public class RefreshableCachedEnumerable<T> : IRefreshableCachedEnumerable<T>, IDisposable
{
    /// <summary>
    /// Instantiates an Empty <see cref="RefreshableCachedEnumerable{T}"/>.
    /// </summary>
    public static RefreshableCachedEnumerable<T> Empty => new([]);
    
    private int ExpectedCount { get; set; }
    
    /// <summary>
    /// Instantiates RefreshableCachedEnumerable with the specified enumerable data source and materialization mode.
    /// </summary>
    /// <param name="enumerable">The underlying enumerable data source.</param>
    /// <param name="mode">The materialization mode to use; defaults to Instant if not provided.</param>
    public RefreshableCachedEnumerable(IEnumerable<T> enumerable,
        EnumerableMaterializationMode mode = EnumerableMaterializationMode.Instant)
    {
       _cache = new List<T>();

       Source = enumerable;
       MaterializationMode = mode;
       ExpectedCount = 0;
       RefreshCache(Source);
    }

    private IEnumerable<T> Source { get; set; }

    /// <summary>
    /// Requests a refresh of the cache by repopulating it from the given source data.
    /// </summary>
    /// <remarks>This method should be used when the underlying data has changed or been updated.
    ///</remarks>
    /// <param name="source">The new source data to use for repopulating the cache.</param>
    public void RefreshCache(IEnumerable<T> source)
    {
        HasBeenMaterialized = false;
        
        if (source is ICollection<T> collection)
        {
            ExpectedCount = collection.Count;
        }
        else
        {
            ExpectedCount = 0;
        }

        switch (MaterializationMode)
        {
            case EnumerableMaterializationMode.Instant:
                _cache.Clear();
                RequestMaterialization();
                break;
            case EnumerableMaterializationMode.Lazy:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Implements the IEnumerable of type T interface to provide a way to iterate over the cached values of type T.
    /// </summary>
    /// <returns>The enumerator to enumerate over the values in this Enumerable.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        if (MaterializationMode == EnumerableMaterializationMode.Lazy)
        {
            if (HasBeenMaterialized == false)
            {
                RequestMaterialization();
            }
        }

        foreach (T item in CachedSource)
        {
            yield return item;
        }
    }

    /// <summary>
    /// Implements the IEnumerable interface to provide a way to iterate over the cached values.
    /// </summary>
    /// <returns>The enumerator to enumerate over the values in this Enumerable.</returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private readonly IList<T> _cache;

    /// <summary>
    /// Gets the internal cache of the enumeration values.
    /// This property provides a read-only view into the cached data.
    /// </summary>
    /// <remarks>Accessing the Cache will materialize the cache if the Cache has not already been materialized.
    /// <para>Accessing the Cache prematurely may be computationally expensive.</para>
    /// </remarks>
    internal IList<T> CachedSource
    {
        get
        {
            if (HasBeenMaterialized == false)
            {
                RequestMaterialization();
            }
                
            return _cache;
        }
    }

    /// <summary>
    /// Indicates whether the cache has been materialized (i.e. populated with data).
    /// </summary>
    /// <remarks>
    /// Use the <see cref="RequestMaterialization"/> method to request materialization of the cache.
    /// <para>Use the <see cref="RefreshCache"/> method to refresh the cache values.</para>
    /// </remarks>
    public bool HasBeenMaterialized { get; private set; }

    /// <summary>
    /// Determines whether the <see cref="RefreshableCachedEnumerable{T}"/> is empty without materializing the source.
    /// </summary>
    /// <remarks>May return false if the source type is an <see cref="IEnumerable{T}"/> and it hasn't yet been materialized.</remarks>
    public bool IsEmpty => ExpectedCount == 0; 

    /// <summary>
    /// Gets the materialization mode used by this enumeration.
    /// The default value is <see cref="EnumerableMaterializationMode.Instant"/>.
    /// </summary>
    public EnumerableMaterializationMode MaterializationMode { get; }

    /// <summary>
    /// Requests that the Cache be materialized from its source.
    /// </summary>
    public void RequestMaterialization()
    {
        if (HasBeenMaterialized == false)
        {
            _cache.Clear();
            foreach (T item in Source)
            {
                _cache.Add(item);
            }

            ExpectedCount = _cache.Count;

            HasBeenMaterialized = true;
        }
    }

    /// <summary>
    /// Disposes of the internal Cache once the Enumerable is to be disposed of. 
    /// </summary>
    public void Dispose()
    {
        _cache.Clear();
    }
}