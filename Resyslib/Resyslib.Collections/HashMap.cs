/*
    Resyslib.Collections
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Resyslib.Collections.Internal.Exceptions;

// ReSharper disable RedundantIfElseBlock
// ReSharper disable RedundantBoolCompare

namespace Resyslib.Collections;

/// <summary>
/// A class to store keys and values that tries to mimic how Java's HashMap works.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class HashMap<TKey, TValue> : IHashMap<TKey, TValue>, IEquatable<HashMap<TKey, TValue>>
{
    // ReSharper disable once InconsistentNaming
    protected readonly Dictionary<TKey, TValue> _dictionary;

    /// <summary>
    /// 
    /// </summary>
    public HashMap()
    {
        _dictionary = new Dictionary<TKey, TValue>();
    }
        
    /// <summary>
    /// Returns whether the HashMap is empty or not.
    /// </summary>
    public bool IsEmpty => Count == 0;

    /// <summary>
    /// Returns the number of KeyValuePairs in the HashMap. 
    /// </summary>
    public int Count => _dictionary.Count;

    /// <summary>
    /// Add a Key and a corresponding Value to the HashMap.
    /// </summary>
    /// <param name="key">The key to be added.</param>
    /// <param name="value">The value to be associated with the key.</param>
    public void Put(TKey key, TValue value)
    {
        if (!ContainsKey(key))
        {
            _dictionary.Add(key, value);
        }

        throw new ArgumentException($"Existing key {key} found with value {GetValue(key)}. Can't put a Key that already exists.");
    }

    /// <summary>
    /// Add a KeyValuePair to the HashMap.
    /// </summary>
    /// <param name="pair">The KeyValuePair to be added.</param>
    public void Put(KeyValuePair<TKey, TValue> pair)
    {
        Put(pair.Key, pair.Value);
    }

    /// <summary>
    /// Adds a Key and a corresponding value to the HashMap only if the key isn't already in use.
    /// If the key is already present in the HashMap no action is taken.
    /// </summary>
    /// <param name="key">The key to be added.</param>
    /// <param name="value">The value to be associated with the key.</param>
    public void PutIfAbsent(TKey key, TValue value)
    {
        if (!ContainsKey(key))
        {
            Put(key, value);
        }
    }

    /// <summary>
    /// Adds a KeyValuePair to the HashMap only if the key isn't already in use.
    /// If the key is already present in the HashMap no action is taken.
    /// </summary>
    /// <param name="pair">The KeyValuePair to be added to the hashmap.</param>
    public void PutIfAbsent(KeyValuePair<TKey, TValue> pair)
    {
        PutIfAbsent(pair.Key, pair.Value);
    }

    /// <summary>
    /// Returns the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key to be checked.</param>
    /// <returns>The value associated with the specified key if the key is in the HashMap.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the key has not been found in the HashMap.</exception>
    public TValue GetValue(TKey key)
    {
        if (ContainsKey(key))
        {
            return _dictionary[key];
        }
            
        throw new KeyNotFoundException();
    }

    /// <summary>
    /// Returns the value associated with the specified key (if the key exists) or the specified default value.
    /// </summary>
    /// <param name="key">The key to be checked.</param>
    /// <param name="defaultValue">The specified value to be returned if the key has not been found.</param>
    /// <returns>The value associated with the key if the key has been found in the HashMap; defaultValue otherwise.</returns>
    public TValue GetValueOrDefault(TKey key, TValue defaultValue)
    {
        try
        {
            if (ContainsKey(key))
            {
                return GetValue(key);
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                return defaultValue;
            }
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Returns an IEnumerable of Keys in the HashMap.
    /// </summary>
    /// <returns>An IEnumerable of Keys in the HashMap.</returns>
    public IEnumerable<TKey> Keys()
    {
        return _dictionary.Keys;
    }

    /// <summary>
    /// Returns an IEnumerable of Values in the HashMap.
    /// </summary>
    /// <returns>An IEnumerable of Values in the HashMap.</returns>
    public IEnumerable<TValue> Values()
    {
        return _dictionary.Values;
    }

    /// <summary>
    /// Returns an IEnumerable of Key Value Pairs in the HashMap.
    /// </summary>
    /// <returns>An IEnumerable of KeyValuePairs in the HashMap.</returns>
    public IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs()
    {
        return _dictionary.ToList();
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary()
    {
        return new ReadOnlyDictionary<TKey, TValue>(_dictionary);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IDictionary<TKey, TValue> ToDictionary()
    {
        return _dictionary;
    }

    /// <summary>
    /// Removes the Key and the value associated with it from the HashMap.
    /// </summary>
    /// <param name="key">The Key to be removed.</param>
    /// <returns>True if the item has been successfully removed from the HashMap; false otherwise.</returns>
    public bool Remove(TKey key)
    {
        if (ContainsKey(key))
        {
            return _dictionary.Remove(key);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Removes the KeyValuePair from the HashMap.
    /// </summary>
    /// <param name="pair">The KeyValuePair to be removed.</param>
    /// <returns>True if the item has been successfully removed from the HashMap; false otherwise.</returns>
    /// <exception cref="KeyValuePairNotFoundException">Thrown if the KeyValuePair isn't found in the HashMap.</exception>
    public bool Remove(KeyValuePair<TKey, TValue> pair)
    {
        if (ContainsKeyValuePair(pair))
        {
            return Remove(pair.Key);
        }

        throw new KeyValuePairNotFoundException(nameof(pair));
    }

    /// <summary>
    /// Removes all instances of a specified Value from the HashMap.
    /// </summary>
    /// <param name="value">The specified value to be removed.</param>
    public void RemoveInstancesOf(TValue value)
    {
        TKey[] keys = _dictionary.Keys.Where(x => _dictionary[x]!.Equals(value)).ToArray();

        // ReSharper disable once ForCanBeConvertedToForeach
        for (int index = 0; index < keys.Length; index++)
        {
            Remove(keys[index]);
        }
    }

    /// <summary>
    /// Replaces the value associated with the specified Key with a specified Value.
    /// </summary>
    /// <param name="key">The key associated with the value to be replaced.</param>
    /// <param name="value">The replacement value.</param>
    /// <returns>True if the value has been replaced; false otherwise.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the specified Key isn't found in the HashMap.</exception>
    public bool Replace(TKey key, TValue value)
    {
        if (ContainsKey(key))
        {
            try
            {
                _dictionary[key] = value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        throw new KeyNotFoundException(nameof(key));
    }

    /// <summary>
    /// Replaces a specified value associated with the specified Key with a specified replacement Value.
    /// </summary> 
    /// <param name="key">The key associated with the value to be replaced and the replacement value.</param>
    /// <param name="oldValue">The value to be replaced.</param>
    /// <param name="newValue">The replacement value.</param>
    /// <returns>True if the oldValue has been replaced with the newValue; false otherwise.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the specified Key isn't found in the HashMap.</exception>
    public bool Replace(TKey key, TValue oldValue, TValue newValue)
    {
        if (ContainsKey(key))
        {
            if (ContainsKeyValuePair(new KeyValuePair<TKey, TValue>(key, oldValue)))
            {
                try
                {
                    _dictionary[key] = newValue;
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        throw new KeyNotFoundException();
    }

    /// <summary>
    /// Clears the contents of the HashMap.
    /// </summary>
    /// 
    public void Clear()
    {
        _dictionary.Clear();
    }

    /// <summary>
    /// Returns whether the HasMap contains a Key or not.
    /// </summary>
    /// <param name="key">The Key to search for.</param>
    /// <returns>True if the HashMap contains the specified key, and false otherwise.</returns>
    public bool ContainsKey(TKey key)
    {
        return _dictionary.ContainsKey(key);
    }

    /// <summary>
    /// Returns whether the HashMap contains a specified value.
    /// </summary>
    /// <param name="value">The specified value.</param>
    /// <returns>True if the HashMap contains the specified value; false otherwise.</returns>
    public bool ContainsValue(TValue value)
    {
        return _dictionary.ContainsValue(value);
    }

    /// <summary>
    /// Determines if the HashMap contains a specified KeyValuePair.
    /// </summary>
    /// <param name="pair">The KeyValuePair to look for.</param>
    /// <returns>True if the HashMap contains the Key specified; false otherwise.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the KeyValuePair is not found within the HashMap.</exception>
    public bool ContainsKeyValuePair(KeyValuePair<TKey, TValue> pair)
    {
        if (ContainsKey(pair.Key))
        {
            return _dictionary[pair.Key]!.Equals(pair.Value);
        }

        throw new KeyValuePairNotFoundException(nameof(_dictionary));
    }

    /// <summary>
    /// Returns whether a HashMap is equal to another HashMap.
    /// </summary>
    /// <param name="hashMap">The HashMap to be compared against.</param>
    /// <returns>True if the compared HashMap is equal to this HashMap; false otherwise.</returns>
    public bool Equals(HashMap<TKey, TValue>? hashMap)
    {
        if (hashMap == null)
        {
            return false;
        }
        else
        {
            List<bool> bools = new List<bool>();

            foreach (KeyValuePair<TKey, TValue> pair in _dictionary)
            {
                if (hashMap.GetValue(pair.Key)!.Equals(pair.Value))
                {
                    bools.Add(true);
                }
                else
                {
                    bools.Add(true);
                }
            }

            return bools.All(b => b == true);
        }
    }

    /// <summary>
    /// Returns whether this HashMap is equal to another object.
    /// </summary>
    /// <param name="obj">The object to be compared against.</param>
    /// <returns>True if the object is equal to this HashMap; false otherwise.</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
            
        if (obj is HashMap<TKey, TValue> map)
        {
            return Equals(map);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        StringBuilder stringBuilder = new();
            
        foreach (KeyValuePair<TKey, TValue> pair in _dictionary)
        {
            stringBuilder.Append($"K{pair.Key},V:{pair.Value}");
            stringBuilder.AppendLine();
        }

        return stringBuilder.ToString().GetHashCode();
    }
}