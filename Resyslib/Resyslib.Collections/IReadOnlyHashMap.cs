/*
    Resyslib.Collections
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

namespace Resyslib.Collections
{
    public interface IReadOnlyHashMap<TKey, TValue>
    {
        int Count { get; }

        TValue GetValue(TKey key);
        TValue GetValueOrDefault(TKey key, TValue defaultValue);


        IEnumerable<TKey> Keys();
        IEnumerable<TValue> Values();
        IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs();
    
    
        IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary();
        IDictionary<TKey, TValue> ToDictionary();
    
        bool ContainsKey(TKey key);
        bool ContainsValue(TValue value);
        bool ContainsKeyValuePair(KeyValuePair<TKey, TValue> pair);
    }
}