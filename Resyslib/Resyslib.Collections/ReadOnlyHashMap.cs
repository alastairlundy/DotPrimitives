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

namespace Resyslib.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ReadOnlyHashMap<TKey, TValue> : IReadOnlyHashMap<TKey, TValue>, IEquatable<ReadOnlyHashMap<TKey, TValue>>
    {
        private readonly ReadOnlyDictionary<TKey, TValue> _dictionary;
    
        public int Count => _dictionary.Count;
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashMap"></param>
        public ReadOnlyHashMap(IHashMap<TKey, TValue> hashMap)
        {
#if NETSTANDARD2_1
            _dictionary = new ReadOnlyDictionary<TKey, TValue>(hashMap.ToDictionary());
#else
             _dictionary = new ReadOnlyDictionary<TKey, TValue?>(hashMap.ToDictionary());
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashMap"></param>
        public ReadOnlyHashMap(HashMap<TKey, TValue> hashMap)
        {
#if NETSTANDARD2_1
            _dictionary = new ReadOnlyDictionary<TKey, TValue>(hashMap.ToDictionary());
#else
             _dictionary = new ReadOnlyDictionary<TKey, TValue?>(hashMap.ToDictionary());
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        public ReadOnlyHashMap(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public TValue GetValue(TKey key)
        {
#if NETSTANDARD2_1
            if (_dictionary.TryGetValue(key, out TValue value))
#else
                if (_dictionary.TryGetValue(key, out TValue? value))
#endif
            {
                return value;
            }
        
            throw new KeyNotFoundException(nameof(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(TKey key, TValue defaultValue)
        {
            try
            {
                return GetValue(key);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TKey> Keys()
        {
            return _dictionary.Keys;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TValue> Values()
        {
            return _dictionary.Values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs()
        {
            return _dictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary()
        {
#if NETSTANDARD2_1
            return new ReadOnlyDictionary<TKey, TValue>(_dictionary);
#else
            return new ReadOnlyDictionary<TKey, TValue?>(_dictionary);
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDictionary<TKey, TValue> ToDictionary()
        {
            return new Dictionary<TKey, TValue>(_dictionary);
        }

        /// <summary>
        /// Returns whether the HasMap contains a Key or not.
        /// </summary>
        /// <param name="key">The Key to search for.</param>
        /// <returns>True if the HashMap contains the specified key, and false otherwise.</returns>
        public bool ContainsKey(TKey key)
        {
            return _dictionary.Keys.Any(k => k != null && k.Equals(key));
        }

        /// <summary>
        /// Returns whether the HashMap contains a specified value.
        /// </summary>
        /// <param name="value">The specified value.</param>
        /// <returns>True if the HashMap contains the specified value; false otherwise.</returns>
        public bool ContainsValue(TValue value)
        {
            return _dictionary.Values.Any(v => v != null && v.Equals(value));
        }

        /// <summary>
        /// Determines if the ReadOnlyHashMap contains a specified KeyValuePair.
        /// </summary>
        /// <param name="pair">The KeyValuePair to look for.</param>
        /// <returns>True if the ReadOnlyHashMap contains the Key specified; false otherwise.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the KeyValuePair is not found within the HashMap.</exception>
        public bool ContainsKeyValuePair(KeyValuePair<TKey, TValue> pair)
        {
            return _dictionary.Any(kv => kv.Value != null && kv.Value.Equals(pair.Value) &&
                                         kv.Key != null && kv.Key.Equals(pair.Key));
        }

        /// <summary>
        /// Returns whether a ReadOnlyHashMap is equal to another HashMap.
        /// </summary>
        /// <param name="other">The ReadOnlyHashMap to be compared against.</param>
        /// <returns>True if the compared HashMap is equal to this ReadOnlyHashMap; false otherwise.</returns>
        public bool Equals(ReadOnlyHashMap<TKey, TValue>? other)
        {
            if (other is null)
            {
                return false;
            }
        
            return _dictionary.Equals(other._dictionary);
        }

        /// <summary>
        /// Returns whether this ReadOnlyHashMap is equal to another object.
        /// </summary>
        /// <param name="obj">The object to be compared against.</param>
        /// <returns>True if the object is equal to this ReadOnlyHashMap; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }
 
            return Equals((ReadOnlyHashMap<TKey, TValue>)obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _dictionary.GetHashCode();
        }
    }
}