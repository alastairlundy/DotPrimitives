/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Resyslib.Collections.Experiments.Localizations;

namespace AlastairLundy.Resyslib.Collections.Generics.HashTables
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>DO NOT USE DIRECTLY unless you are creating an alternative HashTable implementation.
    /// </remarks>
    /// If you need a generics supporting equivalent to Hashtable,
    /// use <see cref="GenericHashTable{TKey,TValue}"/> or <see cref="Dictionary{TKey,TValue}"/>.
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    internal readonly struct GenericHashTableBucket<TKey, TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks> <see cref="GenericHashTable{TKey,TValue}"/> uses the first half of a <see cref="TKey"/>'s hashcode for creating bucket IDs,
        /// and grouping Key Value Pairs in the same bucket.</remarks>
        internal int BucketId { get; }

        /// <summary>
        /// 
        /// </summary>
        internal int Size => Items.Count;
        
        /// <summary>
        /// 
        /// </summary>
        internal readonly List<FlexibleKeyValuePair<TKey, TValue>> Items;
        
        internal readonly List<TKey> Keys;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucketId"></param>
        internal GenericHashTableBucket(int bucketId)
        {
            BucketId = bucketId;
            Items = new List<FlexibleKeyValuePair<TKey, TValue>>();
            Keys = new List<TKey>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="ArgumentException"></exception>
        internal void Add(FlexibleKeyValuePair<TKey, TValue> item)
        {
            if (Items.Select(key => key.Key).Contains(item.Key))
            {
                throw new ArgumentException(Resources.Exceptions_KeyAlreadyExists_Add);
            }
            
            Items.Add(item);
            Keys.Add(item.Key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        internal void Remove(FlexibleKeyValuePair<TKey, TValue> item)
        {
            Items.Remove(item);
            Keys.Remove(item.Key);
        }

        internal void RemoveAt(int index)
        {
            Items.RemoveAt(index);
            Keys.RemoveAt(index);
        }
    }
}