/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:
       
       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.
       
       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
// ReSharper disable ClassNeverInstantiated.Global

// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable UnusedType.Global

namespace AlastairLundy.Resyslib.Collections.Generics.HashMaps
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey">The type representing Keys in the HashMap.</typeparam>
    /// <typeparam name="TValue">The type representing Values in the HashMap.</typeparam>
    public class ReadOnlyHashMap<TKey, TValue> : IReadOnlyHashMap<TKey, TValue>,
        IEquatable<ReadOnlyHashMap<TKey, TValue>> where TKey : notnull
    {
        private readonly IReadOnlyDictionary<TKey, TValue> _dictionary;

        public int Count => _dictionary.Count;
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashMap"></param>
        public ReadOnlyHashMap(IHashMap<TKey, TValue> hashMap)
        {
            _dictionary = new ReadOnlyDictionary<TKey, TValue>(hashMap.ToDictionary());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashMap"></param>
        public ReadOnlyHashMap(IReadOnlyHashMap<TKey, TValue> hashMap)
        {
            _dictionary = new ReadOnlyDictionary<TKey, TValue>(hashMap.ToDictionary());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashMap"></param>
        public ReadOnlyHashMap(HashMap<TKey, TValue> hashMap)
        {
            _dictionary = new ReadOnlyDictionary<TKey, TValue>(hashMap.ToDictionary());
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
        /// Returns the contents of the HashMap instantiated within an IReadonlyDictionary.
        /// </summary>
        /// <returns>The new IReadOnlyDictionary instance.</returns>
        public IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary()
        {
            return new ReadOnlyDictionary<TKey, TValue>(ToDictionary());
        }

        /// <summary>
        /// Returns the contents of the HashMap's internal Dictionary.
        /// </summary>
        /// <returns>The HashMap's internal dictionary.</returns>
        public Dictionary<TKey, TValue> ToDictionary()
        {
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

            foreach (KeyValuePair<TKey, TValue> pair in _dictionary)
            {
                dictionary.Add(pair.Key, pair.Value);
            }

            return dictionary;
        }
        /// <summary>
        /// Returns whether the HasMap contains a Key or not.
        /// </summary>
        /// <param name="key">The Key to search for.</param>
        /// <returns>True if the HashMap contains the specified key, and false otherwise.</returns>
        public bool ContainsKey(TKey key)
        {
            return _dictionary.Keys.Any(k => k.Equals(key));
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
            return _dictionary.Any(kv => kv.Value != null &&
                                         kv.Value.Equals(pair.Value)
                                         && kv.Key.Equals(pair.Key));
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
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new ReadOnlyHashMapEnumerator<TKey, TValue>(this);
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
        /// Computes the hashcode for the ReadOnlyHashMap.
        /// </summary>
        /// <returns>The computed hashcode.</returns>
        public override int GetHashCode()
        {
            return _dictionary.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}