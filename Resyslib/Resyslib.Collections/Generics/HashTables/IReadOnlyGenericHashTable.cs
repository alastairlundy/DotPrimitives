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
        /// Returns an IEnumerable of Key Value Pairs in the GenericHashTable.
        /// </summary>
        /// <returns>An IEnumerable of KeyValuePairs in the GenericHashTable.</returns>
        IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<FlexibleKeyValuePair<TKey, TValue>> FlexibleKeyValuePairs();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IGenericHashTable<TKey, TValue> ToGenericHashTable();
    }
}