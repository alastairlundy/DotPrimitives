/*
    Resyslib.Collections
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System.Collections.Generic;

namespace Resyslib.Collections;

public interface IHashMap<TKey, TValue> : IReadOnlyHashMap<TKey, TValue>
{
    bool IsEmpty { get; }
        
    void Put(TKey key, TValue value);
    void Put(KeyValuePair<TKey, TValue> pair);

    void PutIfAbsent(TKey key, TValue value);
    void PutIfAbsent(KeyValuePair<TKey, TValue> pair);
        
    bool Remove(TKey key);
    bool Remove(KeyValuePair<TKey, TValue> pair);

    void RemoveInstancesOf(TValue value);

    bool Replace(TKey key, TValue value);
    bool Replace(TKey key, TValue oldValue, TValue newValue);

    void Clear();
}