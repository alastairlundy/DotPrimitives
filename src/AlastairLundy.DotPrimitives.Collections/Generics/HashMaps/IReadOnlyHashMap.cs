/*
    AlastairLundy.DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

namespace AlastairLundy.DotPrimitives.Collections.Generics.HashMaps;

/// <summary>
/// The interface for a Read Only HashMap, that does not permit writing to it after instantiation.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface IReadOnlyHashMap<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
{
    /// <summary>
    /// Gets an IEnumerable of all Keys in the HashMap.
    /// </summary>
    /// <returns>The IEnumerable of Keys in the HashMap.</returns>
    IEnumerable<TKey> Keys();
        
    /// <summary>
    /// Gets an IEnumerable of all Values in the HashMap.
    /// </summary>
    /// <returns>The IEnumerable of Values in the HashMap.</returns>
    IEnumerable<TValue> Values();
        
    /// <summary>
    /// Returns an IEnumerable of Key Value Pairs in the HashMap.
    /// </summary>
    /// <returns>An IEnumerable of KeyValuePairs in the HashMap.</returns>
    IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs();
    
    /// <summary>
    /// Returns the contents of the HashMap instantiated within an IReadonlyDictionary.
    /// </summary>
    /// <returns>The new IReadOnlyDictionary instance.</returns>
    IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary();
        
    /// <summary>
    /// Converts the contents of the hash map to a dictionary.
    /// </summary>
    /// <returns>A new dictionary containing the same key-value pairs as this hash map.</returns>
    Dictionary<TKey, TValue> ToDictionary();
}