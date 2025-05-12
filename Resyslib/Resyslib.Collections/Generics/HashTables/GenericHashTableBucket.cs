using AlastairLundy.Resyslib.Collections.Generics.ArrayLists;

namespace AlastairLundy.Resyslib.Collections.Generics.HashTables
{
    internal struct GenericHashTableBucket<TKey, TValue>
    {
        public int BucketCode { get; }

    
        public int Size => Items.Count;

        public GenericHashTableBucket(int bucketCode)
        {
            BucketCode = bucketCode;
            _items = new GenericArrayList<FlexibleKeyValuePair<TKey, TValue>>();
        }

        internal void Add(FlexibleKeyValuePair<TKey, TValue> item)
        {
            _items.Add(item);
        }

        internal void Remove(FlexibleKeyValuePair<TKey, TValue> item)
        {
            _items.Remove(item);
        }

        public GenericArrayList<FlexibleKeyValuePair<TKey, TValue>> Items => _items;
    
        private readonly GenericArrayList<FlexibleKeyValuePair<TKey, TValue>> _items;
    }
}