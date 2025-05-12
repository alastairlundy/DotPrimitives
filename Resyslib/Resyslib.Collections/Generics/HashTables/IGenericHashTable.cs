/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System.Collections.Generic;

namespace AlastairLundy.Resyslib.Collections.Generics.HashTables
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IGenericHashTable<TKey, TValue> : IEnumerable<FlexibleKeyValuePair<TKey, TValue>>
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
        bool IsSynchronized { get; }
        
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
        IGenericHashTable<TKey, TValue> Clone();
        
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
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        void CopyTo(FlexibleKeyValuePair<TKey, TValue>[] array, int arrayIndex);
        
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
        void Add(FlexibleKeyValuePair<TKey, TValue> item);
        
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
        
        IGenericHashTable<TKey, TValue> AsReadOnly();

        List<FlexibleKeyValuePair<TKey, TValue>> ToList();
        FlexibleKeyValuePair<TKey, TValue>[] ToArray();
    }
}