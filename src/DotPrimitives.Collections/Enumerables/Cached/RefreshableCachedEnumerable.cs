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


// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable RedundantEmptySwitchSection

namespace DotPrimitives.Collections.Enumerables.Cached;

/// <summary>
/// Implements the IRefreshableCachedEnumerable interface,
/// providing a way to refresh the cached data and retrieve its values.
/// </summary>
/// <typeparam name="T">The type of elements in the Enumerable.</typeparam>
public class RefreshableCachedEnumerable<T> : IRefreshableCachedEnumerable<T>, IDisposable,
    IEquatable<RefreshableCachedEnumerable<T>>
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
    /// <remarks>Developers should call this method when the underlying data has changed or been updated.
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
            if (!HasBeenMaterialized)
            {
                RequestMaterialization();
            }
        }

        foreach (T item in CachedSource)
        {
            yield return item;
        }
    }

    /// <inheritdoc />
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
            if (!HasBeenMaterialized)
            {
                RequestMaterialization();
            }
                
            return _cache;
        }
    }

    /// <inheritdoc />
    public bool HasBeenMaterialized { get; private set; }

    /// <inheritdoc />
    public bool IsEmpty => ExpectedCount == 0; 

    /// <summary>
    /// Gets the materialization mode used by this enumeration.
    /// The default value is <see cref="EnumerableMaterializationMode.Instant"/>.
    /// </summary>
    public EnumerableMaterializationMode MaterializationMode { get; }

    /// <summary>
    /// Requests that the Cache be materialized from its source.
    /// </summary>
    private void RequestMaterialization()
    {
        if (!HasBeenMaterialized)
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

    /// <inheritdoc />
    public void Dispose()
    {
        _cache.Clear();
    }

    /// <summary>
    /// Determines whether the current instance is equal to another specified <see cref="RefreshableCachedEnumerable{T}"/> instance.
    /// </summary>
    /// <param name="other">The <see cref="RefreshableCachedEnumerable{T}"/> instance to compare with the current instance.</param>
    /// <returns>
    /// True if the specified instance is equal to the current instance; otherwise, false.
    /// </returns>
    public bool Equals(RefreshableCachedEnumerable<T>? other)
    {
        if (other is null) return false;
        
        if (other.HasBeenMaterialized != HasBeenMaterialized)
            return false;

        if (ReferenceEquals(this, other)) return true;
        
        return _cache.Equals(other._cache) &&
               ExpectedCount == other.ExpectedCount &&
               Source.Equals(other.Source) &&
               HasBeenMaterialized == other.HasBeenMaterialized &&
               MaterializationMode == other.MaterializationMode;
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="RefreshableCachedEnumerable{T}"/> are equal.
    /// </summary>
    /// <param name="first">The first instance to compare.</param>
    /// <param name="second">The second instance to compare.</param>
    /// <returns>Returns true if both instances are equal; otherwise, false.</returns>
    public static bool Equals(RefreshableCachedEnumerable<T>? first, RefreshableCachedEnumerable<T>? second)
    {
        if (first is null || second is null)
            return false;
        
        return first.Equals(second);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (obj is RefreshableCachedEnumerable<T> refreshableCachedEnumerable)
            return Equals(refreshableCachedEnumerable);

        return false;
    }

    /// <summary>
    /// Returns the hash code for the current instance.
    /// </summary>
    /// <returns>An integer representing the hash code of the current instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(_cache,
            ExpectedCount, Source, 
            (int)MaterializationMode);
    }

    /// <summary>
    /// Determines whether two instances of <see cref="RefreshableCachedEnumerable{T}"/>are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns>True if the instances are equal; otherwise, false.</returns>
    public static bool operator ==(RefreshableCachedEnumerable<T>? left, RefreshableCachedEnumerable<T>? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two specified RefreshableCachedEnumerable instances are not equal.
    /// </summary>
    /// <param name="left">The first RefreshableCachedEnumerable instance to compare.</param>
    /// <param name="right">The second RefreshableCachedEnumerable instance to compare.</param>
    /// <returns>True if the instances are not equal; otherwise, false.</returns>
    public static bool operator !=(RefreshableCachedEnumerable<T>? left, RefreshableCachedEnumerable<T>? right)
    {
        return !Equals(left, right);
    }
}