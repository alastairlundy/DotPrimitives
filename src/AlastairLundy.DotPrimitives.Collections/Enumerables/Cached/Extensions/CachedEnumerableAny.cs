/*
    AlastairLundy.DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace AlastairLundy.DotPrimitives.Collections.Enumerables.Cached;

/// <summary>
/// A class to hold extension methods for Cached Enumerable types checking for any elements.
/// </summary>
public static class CachedEnumerableAny
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
    /// <param name="cachedEnumerable">The cached enumerable to check.</param>
    /// <typeparam name="T">The type of elements stored in the CachedEnumerable.</typeparam>
    /// <returns>True if any elements are in the source enumerable, false otherwise.</returns>
    public static bool Any<T>(this ICachedEnumerable<T> cachedEnumerable)
    {
        if (cachedEnumerable.IsEmpty == false)
            return true;
        
        return cachedEnumerable.Cache.Count > 0;
    }
    
    /// <summary>
    /// Determines if any items are in the Refreshable Cached Enumerable's source.
    /// </summary>
    /// <remarks>This first attempts to check for any elements without materializing the source enumerable.
    /// <para>If this check is unsuccessful in gauging whether any elements are in the source, the cache is accessed, thereby forcing materialization of the Cache.</para>
    /// <para></para>
    /// <para>If potential premature materialization of the Cache is undesirable,
    /// use <see cref="IRefreshableCachedEnumerable{T}"/>'s IsEmpty property.</para>
    /// </remarks>
    /// <param name="refreshableCachedEnumerable">The refreshable cached enumerable to check.</param>
    /// <typeparam name="T">The type of elements stored in the CachedEnumerable.</typeparam>
    /// <returns>True if any elements are in the source enumerable, false otherwise.</returns>
    public static bool Any<T>(this IRefreshableCachedEnumerable<T> refreshableCachedEnumerable)
    {
        if (refreshableCachedEnumerable.IsEmpty == false)
            return true;

        return refreshableCachedEnumerable.Cache.Count > 0;
    }
}