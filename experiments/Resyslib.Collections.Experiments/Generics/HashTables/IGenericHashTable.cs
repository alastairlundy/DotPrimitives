/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System;
using System.Collections.Generic;

namespace AlastairLundy.Resyslib.Collections.Generics.HashTables
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IGenericHashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>,
        ICloneable
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsFixedSize { get; }
        
        /// <summary>
        /// 
        /// </summary>
        bool IsReadOnly { get; }
        
        /// <summary>
        /// 
        /// </summary>
        ICollection<TKey> Keys { get; }
        
        /// <summary>
        /// 
        /// </summary>
        ICollection<TValue> Values { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        TValue this[TKey key] { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        object SyncRoot { get; }
        
        /// <summary>
        /// 
        /// </summary>
        IEqualityComparer<TKey> EqualityComparer { get; }
        
        /// <summary>
        /// 
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 
        /// </summary>
        void Clear();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        new IGenericHashTable<TKey, TValue> Clone();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainsKey(TKey key);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool ContainsValue(TValue value);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(TKey key, TValue value);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        void Add(KeyValuePair<TKey, TValue> item);
        
        void Remove(TKey key);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Remove(TKey key, TValue value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int GetHash(TKey key);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int GetHashCode();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IGenericHashTable<TKey, TValue> Synchronized();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IReadOnlyGenericHashTable<TKey, TValue> AsReadOnly();

        /// <summary>
        /// Retrieves the list of key-value pairs in this hash table.
        /// </summary>
        /// <returns>A new list containing all key-value pairs in this hash table.</returns>
        List<KeyValuePair<TKey, TValue>> ToList();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        KeyValuePair<TKey, TValue>[] ToArray();
    }
}