/*
    Resyslib.Collections
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Resyslib.Collections.Extensions.HashMaps
{
    public static class HashMapPutExtensions
    {
        /// <summary>
        /// Adds the Key Value Pairs in a Dictionary to a HashMap.
        /// </summary>
        /// <param name="source">The HashMap to have Key Value Pairs added to it.</param>
        /// <param name="dictionaryToAdd">The Dictionary to get the Key Value Pairs from.</param>
        /// <typeparam name="TKey">The type of Key in the HashMap and Dictionary.</typeparam>
        /// <typeparam name="TValue">The type of Value in the HashMap and Dictionary.</typeparam>
        public static void PutRange<TKey, TValue>(this HashMap<TKey, TValue> source, Dictionary<TKey, TValue> dictionaryToAdd)
        {
            foreach (KeyValuePair<TKey, TValue> pair in dictionaryToAdd)
            {
                if (source.Count == int.MaxValue)
                {
                    throw new OverflowException($"{nameof(source)}  has reached the maximum size of {int.MaxValue} and cannot be added to.");
                }
                else if (dictionaryToAdd.Count == int.MaxValue)
                {
                    throw new OverflowException($"{nameof(dictionaryToAdd)}  has reached the maximum size of {int.MaxValue} and cannot be added to {nameof(source)}.");
                }
                
                source.Put(pair);
            }
        }
        
        /// <summary>
        /// Adds items in an IEnumerable to a HashMap
        /// </summary>
        /// <typeparam name="TKey">The type of the Keys used.</typeparam>
        /// <typeparam name="TValue">The type of the Values used.</typeparam>
        /// <param name="source">The HashMap to be added to.</param>
        /// <param name="enumerable">The IEnumerable of items to add to the HashMap.</param>
        public static void PutRange<TKey, TValue>(this HashMap<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
        {
            KeyValuePair<TKey, TValue>[] keyValuePairs = enumerable as KeyValuePair<TKey, TValue>[] ?? enumerable.ToArray();
            
            foreach(KeyValuePair<TKey, TValue> pair in keyValuePairs)
            {
                if (source.Count == int.MaxValue)
                {
                    throw new OverflowException($"{nameof(source)} has reached the maximum size of {int.MaxValue} and cannot be added to.");
                }
                else if (keyValuePairs.Length == int.MaxValue)
                {
                    throw new OverflowException($"{nameof(enumerable)} has reached the maximum size of {int.MaxValue} and cannot be added to {nameof(source)}.");
                }

                if (pair.Value != null)
                {
                    source.Put(pair);
                }
            }
        }
    }
}