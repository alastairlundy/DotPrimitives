using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AlastairLundy.Resyslib.Collections.Generics.HashTables
{
    public class GenericHashTable<TKey, TValue> : IGenericHashTable<TKey, TValue>
    {
    
    
        public IEnumerator<FlexibleKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }

        public bool IsFixedSize { get; }
    
        public bool IsReadOnly { get; }
    
        public bool IsSynchronized { get; }
    
        public ICollection<TKey> Keys { get; }
    
        public ICollection<TValue> Values { get; }

        public TValue this[TKey key]
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public object SyncRoot { get; }
        public EqualityComparer<FlexibleKeyValuePair<TKey, TValue>> EqualityComparer { get; }
        public int Count { get; }
        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public object Clone()
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsValue(TValue value)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public void Add(TKey key, TValue value)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(TKey key, TValue value)
        {
            throw new System.NotImplementedException();
        }

        public int GetHash(TKey key)
        {
            throw new System.NotImplementedException();
        }

        public IGenericHashTable<TKey, TValue> Synchronized()
        {
            throw new System.NotImplementedException();
        }
    }
}