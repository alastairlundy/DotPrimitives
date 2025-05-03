

using System.Collections.Generic;

using System.Runtime.Serialization;

namespace AlastairLundy.Resyslib.Collections.Generics.HashTables
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IGenericHashTable<TKey, TValue> : IEnumerable<FlexibleKeyValuePair<TKey, TValue>>, ISerializable
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
        EqualityComparer<(TKey, TValue)> EqualityComparer { get; }
        
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
        object Clone();
        
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
    }
}