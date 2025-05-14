/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

namespace AlastairLundy.Resyslib.Collections.Generics.Enumerables
{ 
    /// <summary>
    /// Defines an interface for an Enumerable that can be cached and materialized on demand,
    /// with the ability to refresh the cache.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumeration.</typeparam>
    public interface IRefreshableCachedEnumerable<T> : ICachedEnumerable<T>
    {
        
        /// <summary>
        /// Requests a refresh of the internal cache by repopulating it from the given source data.
        /// This method is typically used when the underlying data has changed or been updated.
        /// </summary>
        /// <param name="source">The new source data to use for repopulating the cache.</param>
        void RefreshCache(IEnumerable<T> source);
    }
}