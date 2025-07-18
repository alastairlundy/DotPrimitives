﻿/*
    AlastairLundy.DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System.Collections.Generic;

// ReSharper disable UnusedMemberInSuper.Global

namespace AlastairLundy.DotPrimitives.Collections.HashMaps;

/// <summary>
///     Defines a generic interface for a hash map data structure.
/// </summary>
/// <typeparam name="TKey">The type of keys used in the hash map.</typeparam>
/// <typeparam name="TValue">The type of values stored in the hash map.</typeparam>
public interface IHashMap<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    /// <summary>
    /// Gets the number of key-value pairs in the hash map.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Indicates whether the hash map contains no items.
    /// </summary>
    /// <returns>True if the hash map contains no items; otherwise, false.</returns>
    bool IsEmpty { get; }

    /// <summary>
    /// Retrieves the value associated with the specified key from the hash map.
    /// </summary>
    /// <param name="key">The key for which to retrieve the value.</param>
    /// <returns>The value associated with the specified key, or a default value if the key is not present in the hash map.</returns>
    TValue GetValue(TKey key);

    /// <summary>
    /// Retrieves the value associated with the specified key from the hash map, returning a default value if the key is
    /// not present.
    /// </summary>
    /// <param name="key">The key for which to retrieve the value.</param>
    /// <param name="defaultValue">The default value to return if the key is not present in the hash map.</param>
    /// <returns>The value associated with the specified key, or the default value if the key is not present.</returns>
    TValue GetValueOrDefault(TKey key, TValue defaultValue);

    /// <summary>
    /// Determines whether the specified key is contained in the hash map.
    /// </summary>
    /// <param name="key">The key to search for.</param>
    /// <returns>True if the specified key is present in the hash map; false otherwise.</returns>
    bool ContainsKey(TKey key);

    /// <summary>
    /// Determines whether the specified value is contained in the hash map.
    /// </summary>
    /// <param name="value">The value to search for.</param>
    /// <returns>True if the specified value is present in the hash map; false otherwise</returns>
    bool ContainsValue(TValue value);

    /// <summary>
    /// Determines whether a key-value pair matching the specified pair is contained in the hash map.
    /// </summary>
    /// <param name="pair">The key-value pair to search for in the hash map.</param>
    /// <returns>True if a key-value pair matching the specified pair is present in the hash map; otherwise, false.</returns>
    bool ContainsKeyValuePair(KeyValuePair<TKey, TValue> pair);

    /// <summary>
    /// Inserts a key-value pair into the hash map.
    /// </summary>
    /// <param name="key">The key to insert or update in the hash map.</param>
    /// <param name="value">The value associated with the specified key.</param>
    void Put(TKey key, TValue value);

    /// <summary>
    /// Inserts a key-value pair into the hash map.
    /// </summary>
    /// <param name="pair">The key-value pair to insert into the hash map.</param>
    void Put(KeyValuePair<TKey, TValue> pair);

    /// <summary>
    /// Puts a key-value pair into the HashMap if it does not already exist.
    /// </summary>
    /// <param name="key">The key to be added.</param>
    /// <param name="value">The value associated with the key.</param>
    void PutIfAbsent(TKey key, TValue value);
        
    /// <summary>
    /// Puts a key-value pair into the HashMap if it does not already exist.
    /// </summary>
    /// <param name="pair">The key-value pair to be added.</param>
    void PutIfAbsent(KeyValuePair<TKey, TValue> pair);
        
    /// <summary>
    /// Replaces the value in a key-value pair in the hash map with a different value.
    /// </summary>
    /// <param name="key">The key to insert or update in the hash map.</param>
    /// <param name="value">The value to be associated with the specified key.</param>
    /// <returns>True if the operation was successful; otherwise, false.</returns>
    bool Replace(TKey key, TValue value);
        
    /// <summary>
    /// Removes a key from the HashMap if it exists.
    /// </summary>
    /// <param name="key">The key to be removed.</param>
    /// <returns>True if the key was found and removed, false otherwise.</returns>
    bool Remove(TKey key);

    /// <summary>
    /// Removes a key-value pair from the HashMap if it exists.
    /// </summary>
    /// <param name="pair">The key-value pair to be removed.</param>
    /// <returns>True if the pair was found and removed, false otherwise.</returns>
    bool Remove(KeyValuePair<TKey, TValue> pair);
        
    /// <summary>
    ///  Removes all occurrences of the specified value in the hash map.
    /// </summary>
    /// <param name="value">The value to be removed.</param>
    void RemoveInstancesOf(TValue value);
        
    /// <summary>
    /// Updates a key-value pair in the hash map, replacing its current value with the new one if the key already exists.</summary>
    /// <param name="key">The key to be updated in the hash map.</param>
    /// <param name="oldValue">The old value currently associated with the specified key.</param>
    /// <param name="newValue">The new value to be associated with the specified key.</param>
    /// <returns>True if the operation was successful; otherwise, false.</returns>
    bool Replace(TKey key, TValue oldValue, TValue newValue);
        
    /// <summary>
    /// Removes all items from the hash map.
    /// </summary>
    void Clear();

    /// <summary>
    /// Converts the contents of the hash map to a dictionary.
    /// </summary>
    /// <returns>A new dictionary containing the same key-value pairs as this hash map.</returns>
    Dictionary<TKey, TValue> ToDictionary();
}