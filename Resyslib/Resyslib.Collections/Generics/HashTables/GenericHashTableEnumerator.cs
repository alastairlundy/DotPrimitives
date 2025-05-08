/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.Resyslib.Collections.Generics.HashTables
{
    /// <summary>
    /// An enumerator for a Generic HashTable that provides access to its key-value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the HashTable.</typeparam>
    /// <typeparam name="TValue">The type of values in the HashTable.</typeparam>
    /// <remarks>This enumerator enumerates the GenericHashTable without re-ordering the elements within it.</remarks>
    public struct GenericHashTableEnumerator<TKey, TValue> :
        IEnumerator<FlexibleKeyValuePair<TKey, TValue>> where TKey : notnull
    {
        private FlexibleKeyValuePair<TKey, TValue>[] _hashTable;

        private int _position = -1;
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashTable"></param>
        public GenericHashTableEnumerator(GenericHashTable<TKey, TValue> hashTable)
        {
            _hashTable = hashTable.ToArray();
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            _position++;

            return (_position < _hashTable.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            _position = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        FlexibleKeyValuePair<TKey, TValue> IEnumerator<FlexibleKeyValuePair<TKey, TValue>>.Current => _hashTable[_position];

        /// <summary>
        /// 
        /// </summary>
        object? IEnumerator.Current => _hashTable[_position];

        /// <summary>
        /// Releases any resources used by this instance of the enumerator.
        /// </summary>
        /// <remarks>The default implementation disposes of the internal keys, but derived classes should override this method to release any resources that
        /// they own.</remarks>
        public void Dispose()
        {
            _hashTable = [];
        }
    }
}