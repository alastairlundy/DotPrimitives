/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace AlastairLundy.Resyslib.Collections.Generics.HashTables
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ReadOnlyGenericHashTable<TKey,TValue> : IReadOnlyGenericHashTable<TKey, TValue>
    {
        private readonly IGenericHashTable<TKey,TValue> _hashTable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public ReadOnlyGenericHashTable(IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            KeyValuePair<TKey, TValue>[] sourceArray = source.ToArray();
        
            _hashTable = new GenericHashTable<TKey, TValue>(
                isReadOnly: true,
                isFixedSize: true,
                comparer: EqualityComparer<TKey>.Default,
                source: sourceArray);
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashTable"></param>
        public ReadOnlyGenericHashTable(IGenericHashTable<TKey, TValue> hashTable)
        {
            _hashTable = hashTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashTable"></param>
        public ReadOnlyGenericHashTable(GenericHashTable<TKey, TValue> hashTable)
        {
            _hashTable = hashTable;
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _hashTable.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count => _hashTable.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TKey> Keys() => _hashTable.Keys;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TValue> Values() => _hashTable.Values;
    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs()
        {
            return from item in _hashTable
                select item;
        }

        /// <summary>
        /// Converts this hash table to a generic implementation, exposing its underlying data.
        /// Note that this operation does not modify the original collection and returns a new instance.
        /// </summary>
        /// <returns>A new instance of <see cref="IGenericHashTable{TKey,TValue}"/> containing the same key-value pairs as this object.</returns>
        [Pure]
        public IGenericHashTable<TKey, TValue> ToGenericHashTable()
        {
            return new GenericHashTable<TKey, TValue>(isReadOnly: false,
                isFixedSize: false,
                comparer: _hashTable.EqualityComparer,
                source: _hashTable);
        }
    }
}