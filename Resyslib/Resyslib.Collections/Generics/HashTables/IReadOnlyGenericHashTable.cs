/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

namespace AlastairLundy.Resyslib.Collections.Generics.HashTables
{
    /// <summary>
    /// Represents a Generic Hash Table that provides read-only access to its key-value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of keys stored in this Generic Hash Table.</typeparam>
    /// <typeparam name="TValue">The type of values stored in this Generic Hash Table.</typeparam>
    public interface IReadOnlyGenericHashTable<TKey, TValue> : IReadOnlyCollection<FlexibleKeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// Gets an IEnumerable of all Keys in the GenericHashTable.
        /// </summary>
        /// <returns>The IEnumerable of Keys in the GenericHashTable.</returns>
        IEnumerable<TKey> Keys();
        
        /// <summary>
        /// Gets an IEnumerable of all Values in the GenericHashTable.
        /// </summary>
        /// <returns>The IEnumerable of Values in the GenericHashTable.</returns>
        IEnumerable<TValue> Values();
        
        /// <summary>
        /// Returns an IEnumerable collection of key-value pairs from this Read Only Generic HashTable.
        /// </summary>
        /// <returns>An IEnumerable collection of <see cref="KeyValuePair{TKey,TValue}"/> objects.</returns>
        IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs();
        
        /// <summary>
        /// Returns an IEnumerable collection of key-value pairs from this Read Only Generic HashTable.
        /// </summary>
        /// <returns>An IEnumerable collection of <see cref="FlexibleKeyValuePair{TKey,TValue}"/> objects.</returns>
        IEnumerable<FlexibleKeyValuePair<TKey, TValue>> FlexibleKeyValuePairs();

        /// <summary>
        /// Creates a new instance of the GenericHashTable from this read-only version.
        /// </summary>
        /// <returns>A new instance of the GenericHashTable.</returns>
        IGenericHashTable<TKey, TValue> ToGenericHashTable();
    }
}