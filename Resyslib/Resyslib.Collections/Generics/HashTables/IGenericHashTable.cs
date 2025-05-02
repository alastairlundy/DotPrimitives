

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
        bool IsFixedSize { get; }
        bool IsReadOnly { get; }
        
        bool IsSynchronized { get; }
        
        ICollection<TKey> Keys { get; }
        ICollection<TValue> Values { get; }
        
        TValue this[TKey key] { get; set; }
        
        object SyncRoot { get; }
        
        EqualityComparer<(TKey, TValue)> EqualityComparer { get; }
        
        int Count { get; }

        void Clear();
        object Clone();
        bool ContainsKey(TKey key);
        bool ContainsValue(TValue value);
        
        void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex);

        void Add(TKey key, TValue value);
        void Remove(TKey key, TValue value);

        int GetHash(TKey key);

        int GetHashCode();

        IGenericHashTable<TKey, TValue> Synchronized();
    }
}