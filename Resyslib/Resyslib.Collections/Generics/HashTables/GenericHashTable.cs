/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

using AlastairLundy.Resyslib.Collections.Generics.ArrayLists;
using AlastairLundy.Resyslib.Collections.Internal.Localizations;

namespace AlastairLundy.Resyslib.Collections.Generics.HashTables
{
    public class GenericHashTable<TKey, TValue> : IGenericHashTable<TKey, TValue>
    {
        private const int DefaultInitialCapacity = 10;

        private readonly GenericArrayList<GenericHashTableBucket<TKey, TValue>> _buckets;

        private readonly GenericArrayList<TKey> _keys;

        private readonly GenericArrayList<TValue> _values;
        
        /// <summary>
        /// 
        /// </summary>
        public GenericHashTable()
        {
            SyncRoot = Guid.NewGuid();
            IsFixedSize = false;
            IsReadOnly = false;
            IsSynchronized = false;
            
            _keys = new GenericArrayList<TKey>();
            _values = new GenericArrayList<TValue>();
            _buckets = new GenericArrayList<GenericHashTableBucket<TKey, TValue>>();
            
            EqualityComparer = EqualityComparer<TKey>.Default;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparer"></param>
        public GenericHashTable(IEqualityComparer<TKey> comparer)
        {
            SyncRoot = Guid.NewGuid();
            IsFixedSize = false;
            IsReadOnly = false;
            IsSynchronized = false;
            
            _keys = new GenericArrayList<TKey>();
            _values = new GenericArrayList<TValue>();
            _buckets = new GenericArrayList<GenericHashTableBucket<TKey, TValue>>();
            
            EqualityComparer = comparer;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSynchronized"></param>
        /// <param name="isReadOnly"></param>
        /// <param name="isFixedSize"></param>
        /// <param name="initialCapacity"></param>
        public GenericHashTable(bool isSynchronized, bool isReadOnly, bool isFixedSize, int initialCapacity = DefaultInitialCapacity)
        {
            EqualityComparer = EqualityComparer<TKey>.Default;
            SyncRoot = Guid.NewGuid();
            IsSynchronized = isSynchronized;
            IsReadOnly = isReadOnly;
            IsFixedSize = isFixedSize;
            
            _keys = new GenericArrayList<TKey>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
            _values = new GenericArrayList<TValue>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
            
            _buckets = new GenericArrayList<GenericHashTableBucket<TKey, TValue>>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSynchronized"></param>
        /// <param name="isReadOnly"></param>
        /// <param name="isFixedSize"></param>
        /// <param name="comparer"></param>
        /// <param name="initialCapacity"></param>
        public GenericHashTable(bool isSynchronized, bool isReadOnly, bool isFixedSize, IEqualityComparer<TKey> comparer, int initialCapacity = DefaultInitialCapacity)
        {
            EqualityComparer = comparer;
            SyncRoot = Guid.NewGuid();
            IsSynchronized = isSynchronized;
            IsReadOnly = isReadOnly;
            IsFixedSize = isFixedSize;
            
            _keys = new GenericArrayList<TKey>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
            _values = new GenericArrayList<TValue>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
            
            _buckets = new GenericArrayList<GenericHashTableBucket<TKey, TValue>>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSynchronized"></param>
        /// <param name="isReadOnly"></param>
        /// <param name="isFixedSize"></param>
        /// <param name="initialCapacity"></param>
        /// <param name="source"></param>
        public GenericHashTable(bool isSynchronized, bool isReadOnly, bool isFixedSize, int initialCapacity,
            IEnumerable<FlexibleKeyValuePair<TKey, TValue>> source)
        {
            EqualityComparer = EqualityComparer<TKey>.Default;
            SyncRoot = Guid.NewGuid();
            IsSynchronized = isSynchronized;
            IsReadOnly = isReadOnly;
            IsFixedSize = isFixedSize;
            
            _keys = new GenericArrayList<TKey>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
            _values = new GenericArrayList<TValue>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
            
            _buckets = new GenericArrayList<GenericHashTableBucket<TKey, TValue>>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);

            foreach (FlexibleKeyValuePair<TKey, TValue> pair in source)
            {
                Add(pair);   
            }
        }
        
        public GenericHashTable(bool isSynchronized, bool isReadOnly, bool isFixedSize, int initialCapacity,
            IEnumerable<FlexibleKeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> comparer)
        {
            EqualityComparer = comparer;
            SyncRoot = Guid.NewGuid();
            IsSynchronized = isSynchronized;
            IsReadOnly = isReadOnly;
            IsFixedSize = isFixedSize;
            
            _keys = new GenericArrayList<TKey>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
            _values = new GenericArrayList<TValue>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
            
            _buckets = new GenericArrayList<GenericHashTableBucket<TKey, TValue>>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);

            foreach (FlexibleKeyValuePair<TKey, TValue> pair in source)
            {
                Add(pair);   
            }
        }
        
        private int GetBucketIndex(int bucketId)
        {
            for (int index = 0; index < _buckets.Count; index++)
            {
                if (_buckets[index].BucketId.Equals(bucketId))
                {
                    return index;
                }
            }

            return -1;
        }
        
        private int GetBucketId(TKey key)
        {
            return GetBucketId(GetHash(key));
        }
        
        private int GetBucketId(int hashCode)
        {
            string hashCodeString = hashCode.ToString();
            IEnumerable<char> newCode = hashCodeString.Take(hashCodeString.Length / 2);

            int id = int.Parse(string.Join("", newCode));

            return id;
        }

        private void SetValue(TKey key, TValue newValue)
        {
            if (key is null)
            {
                throw new NullReferenceException();
            }
            
            int requiredBucketId = GetBucketId(key.GetHashCode());

            int bucketIndex = GetBucketIndex(requiredBucketId);

            for (int i = 0; i <  _buckets[bucketIndex].Items.Count; i++)
            {
               if(key.Equals(_buckets[bucketIndex].Items[i].Key))
               {
                  _buckets[bucketIndex].Items.RemoveAt(i);
                  _buckets[bucketIndex].Items.Insert(i, new FlexibleKeyValuePair<TKey, TValue>(key, newValue));
                  return;
               }
            }
        }
        
        private TValue GetValueFromKey(TKey key)
        {
            if (key is null)
            {
                throw new NullReferenceException();
            }
            
            int requiredBucketId = GetBucketId(key.GetHashCode());

            int bucketIndex = GetBucketIndex(requiredBucketId);

            foreach (FlexibleKeyValuePair<TKey, TValue> item in _buckets[bucketIndex].Items)
            {
                TKey itemKey = item.Key;
                
                if (itemKey is not null && itemKey.Equals(key))
                {
                    return item.Value;
                }
            }
            
            throw new KeyNotFoundException();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<FlexibleKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new GenericArrayListEnumerator<FlexibleKeyValuePair<TKey, TValue>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        private int _count;

        /// <summary>
        /// 
        /// </summary>
        public bool IsFixedSize { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSynchronized { get; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<TKey> Keys => _keys;

        /// <summary>
        /// 
        /// </summary>
        public ICollection<TValue> Values => _values;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public TValue this[TKey key]
        {
            get => GetValueFromKey(key);
            set => SetValue(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public object SyncRoot { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public IEqualityComparer<TKey> EqualityComparer { get; }

        /// <summary>
        /// 
        /// </summary>
        public int Count => _count;
        
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _buckets.Clear();
            _keys.Clear();
            _values.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IGenericHashTable<TKey, TValue> Clone()
        {
            IGenericHashTable<TKey, TValue> clone = new GenericHashTable<TKey, TValue>();

            foreach (FlexibleKeyValuePair<TKey, TValue> pair in this)
            {
                clone.Add(pair.Key, pair.Value);
            }

            return clone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool ContainsKey(TKey key)
        {
            if (key is null)
            {
                throw new NullReferenceException();
            }
            
            int requiredBucketId = GetBucketId(key.GetHashCode());

            int bucketIndex = GetBucketIndex(requiredBucketId);
            
            foreach (FlexibleKeyValuePair<TKey, TValue> item in _buckets[bucketIndex].Items)
            {
                if (key.Equals(item.Key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool ContainsValue(TValue value)
        {
            if (value is null)
            {
                throw new NullReferenceException();
            }
            
            foreach (TValue val in _values)
            {
                return value.Equals(val);
            }

            return false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FlexibleKeyValuePair<TKey, TValue>> ToList()
        {
            List<FlexibleKeyValuePair<TKey, TValue>> list = new List<FlexibleKeyValuePair<TKey, TValue>>(); 

            foreach (FlexibleKeyValuePair<TKey, TValue> pair in this)
            {
                list.Add(pair);
            }

            return list;
        }
        
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FlexibleKeyValuePair<TKey, TValue>[] ToArray()
        {
            FlexibleKeyValuePair<TKey, TValue>[] output = new FlexibleKeyValuePair<TKey, TValue>[Count];

            int index = 0;
            
            foreach (GenericHashTableBucket<TKey, TValue> bucket in _buckets)
            {
                bucket.Items.CopyTo(output, index);
                index += bucket.Items.Count;
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            KeyValuePair<TKey, TValue>[] tempArray = this
                .Select(pair => new KeyValuePair<TKey, TValue>(pair.Key, pair.Value)).ToArray();

            int limit = array.Length - arrayIndex;
           
            for (int i = arrayIndex; i < limit; i++)
            {
                array[i] = tempArray[i];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(FlexibleKeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            FlexibleKeyValuePair<TKey, TValue>[] tempArray = ToArray();

           int limit = array.Length - arrayIndex;
           
           for (int i = arrayIndex; i < limit; i++)
           {
               array[i] = tempArray[i];
           }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            Add(new FlexibleKeyValuePair<TKey, TValue>(key, value));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void Add(FlexibleKeyValuePair<TKey, TValue> item)
        {
            if (IsReadOnly || IsFixedSize)
            {
                throw new InvalidOperationException(Resources.Exceptions_CannotModifyReadOnlyOrFixedCollection);
            }
            
            int bucketId = GetBucketId(item.Key);
            
            int bucketIndex = GetBucketIndex(bucketId);

            if (bucketIndex == -1)
            {
                GenericHashTableBucket<TKey, TValue> bucket = new GenericHashTableBucket<TKey, TValue>(bucketId);
                
                bucket.Add(item);
                
                _buckets.Add(bucket);
                _keys.Add(item.Key);
                _values.Add(item.Value);
                _count++;
            }
            else
            {
                if (_buckets[bucketIndex].Items.Contains(new FlexibleKeyValuePair<TKey, TValue>(item.Key, item.Value)))
                {
                    throw new ArgumentException(Resources.Exceptions_KeyAlreadyExists_Add);
                }
                else
                {
                    _buckets[bucketIndex].Add(new FlexibleKeyValuePair<TKey, TValue>(item.Key, item.Value));
                    _keys.Add(item.Key);
                    _values.Add(item.Value);
                    _count++;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void Remove(TKey key)
        {
            if (key is null)
            {
                throw new NullReferenceException();
            }

            if (IsReadOnly || IsFixedSize)
            {
                throw new InvalidOperationException(Resources.Exceptions_CannotModifyReadOnlyOrFixedCollection);
            }
            
            int bucketId = GetBucketId(key.GetHashCode());
            
            int bucketIndex = GetBucketIndex(bucketId);

            for (int index = 0; index < _buckets[bucketIndex].Items.Count; index++)
            {
                FlexibleKeyValuePair<TKey, TValue> item = _buckets[bucketIndex].Items[index];

                if (key.Equals(item.Key))
                {
                    _buckets[bucketIndex].Items.RemoveAt(index);
                    _count--;
                    break;
                }
            }

            _keys.Remove(key);
            
            if (_buckets[bucketIndex].Size == 0)
            {
                _buckets.RemoveAt(bucketIndex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NullReferenceException">Thrown if the GenericHashTable is read-only or is a Fixed Size.</exception>
        public void Remove(TKey key, TValue value)
        {
            if (IsFixedSize || IsReadOnly)
            {
                throw new InvalidOperationException();
                throw new InvalidOperationException(Resources.Exceptions_CannotModifyReadOnlyOrFixedCollection);
            }
            
            if (key is null)
            {
                throw new NullReferenceException();
            }

            if (value is null)
            {
                throw new NullReferenceException();
            }
            
            if (_keys.Contains(key))
            {
                _keys.Remove(key);
            }

            int bucketId = GetBucketId(key.GetHashCode());
            
            int bucketIndex = GetBucketIndex(bucketId);

            for (int index = 0; index < _buckets[bucketIndex].Items.Count; index++)
            {
                FlexibleKeyValuePair<TKey, TValue> item = _buckets[bucketIndex].Items[index];

                if (key.Equals(item.Key) && value.Equals(item.Value))
                {
                    _buckets[bucketIndex].Items.RemoveAt(index);
                    _count--;
                    break;
                }
            }
            
            int firstIndexofVal = _values.IndexOf(value);
            
            _values.RemoveAt(firstIndexofVal);

            if (_buckets[bucketIndex].Size == 0)
            {
                _buckets.RemoveAt(bucketIndex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public int GetHash(TKey key)
        {
            if (key is null)
            {
                throw new NullReferenceException();
            }
            
            return EqualityComparer.GetHashCode(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Pure]
        public IGenericHashTable<TKey, TValue> Synchronized()
        {
            return new GenericHashTable<TKey, TValue>(isSynchronized: true,
                isReadOnly: IsReadOnly,
                initialCapacity: Count,
                isFixedSize: IsFixedSize,
                source: this,
                comparer:EqualityComparer
            );
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Pure]
        public IGenericHashTable<TKey, TValue> AsReadOnly()
        {
            return new GenericHashTable<TKey, TValue>(isSynchronized: false,
                isReadOnly: true,
                initialCapacity: Count,
                isFixedSize: true,
                source: this,
                comparer: EqualityComparer
            );
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}