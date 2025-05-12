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
using System.Linq;
using System.Runtime.Serialization;

using AlastairLundy.Resyslib.Collections.Generics.ArrayLists;

namespace AlastairLundy.Resyslib.Collections.Generics.HashTables
{
    public class GenericHashTable<TKey, TValue> : IGenericHashTable<TKey, TValue>
    {
        private const int DefaultInitialCapacity = 10;

        private readonly GenericArrayList<GenericHashTableBucket<TKey, TValue>> _buckets;

        private readonly GenericArrayList<TKey> _keys;

        private readonly GenericArrayList<TValue> _values;
        
        public GenericHashTable()
        {
            IsFixedSize = false;
            IsReadOnly = false;
            IsSynchronized = false;
            
            _keys = new GenericArrayList<TKey>();
            _values = new GenericArrayList<TValue>();
            _buckets = new GenericArrayList<GenericHashTableBucket<TKey, TValue>>();
        }

        public GenericHashTable(bool isSynchronized, bool isReadOnly, bool isFixedSize, int initialCapacity = DefaultInitialCapacity)
        {
            IsSynchronized = isSynchronized;
            IsReadOnly = isReadOnly;
            IsFixedSize = isFixedSize;
            
            _keys = new GenericArrayList<TKey>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
            _values = new GenericArrayList<TValue>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
            
            _buckets = new GenericArrayList<GenericHashTableBucket<TKey, TValue>>(isReadOnly, isFixedSize, isSynchronized, initialCapacity);
        }
        
        public GenericHashTable(bool isSynchronized, bool isReadOnly, bool isFixedSize, int initialCapacity, IEnumerable<FlexibleKeyValuePair<TKey, TValue>> source)
        {
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
        
        private int GetBucketIndex(int bucketCode)
        {
            for (int index = 0; index < _buckets.Count; index++)
            {
                if (_buckets[index].BucketCode.Equals(bucketCode))
                {
                    return index;
                }
            }

            return -1;
        }
        
        private int GetBucketCode(TKey key)
        {
            return GetBucketCode(GetHash(key));
        }
        
        private int GetBucketCode(int hashCode)
        {
            string hashCodeString = hashCode.ToString();
            IEnumerable<char> newCode = hashCodeString.Take(hashCodeString.Length / 2);

            int code = int.Parse(string.Join("", newCode));

            return code;
        }

        private void SetValue(TKey key, TValue newValue)
        {
            if (key is null)
            {
                throw new NullReferenceException();
            }
            
            int requiredBucketCode = GetBucketCode(key.GetHashCode());

            int bucketIndex = GetBucketIndex(requiredBucketCode);

            
        }
        
        private TValue GetValueFromKey(TKey key)
        {
            if (key is null)
            {
                throw new NullReferenceException();
            }
            
            int requiredBucketCode = GetBucketCode(key.GetHashCode());

            int bucketIndex = GetBucketIndex(requiredBucketCode);

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

        public TValue this[TKey key]
        {
            get => GetValueFromKey(key);
            set => SetValue(key, value);
        }

        public object SyncRoot { get; }
        
        public EqualityComparer<FlexibleKeyValuePair<TKey, TValue>> EqualityComparer { get; }


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
            
            int requiredBucketCode = GetBucketCode(key.GetHashCode());

            int bucketIndex = GetBucketIndex(requiredBucketCode);
            
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
            List<FlexibleKeyValuePair<TKey, TValue>> list = ToList();

            for (int i = 0; i < list.Count; i++)
            {
                output[i] = new FlexibleKeyValuePair<TKey, TValue>(list[i].Key, list[i].Value);
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
            List<KeyValuePair<TKey, TValue>> list = ToList()
                .Select(pair => new KeyValuePair<TKey, TValue>(pair.Key, pair.Value)).ToList();

            int limit = array.Length - arrayIndex;
           
            for (int i = arrayIndex; i < limit; i++)
            {
                array[i] = list[i];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(FlexibleKeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
           List<FlexibleKeyValuePair<TKey, TValue>> list = this.ToList();

           int limit = array.Length - arrayIndex;
           
           for (int i = arrayIndex; i < limit; i++)
           {
               array[i] = list[i];
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
        public void Add(FlexibleKeyValuePair<TKey, TValue> item)
        {
            int bucketCode = GetBucketCode(item.Key);
            
            int bucketIndex = GetBucketIndex(bucketCode);

            if (bucketIndex == -1)
            {
                GenericHashTableBucket<TKey, TValue> bucket = new GenericHashTableBucket<TKey, TValue>(bucketCode);
                
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
            
            int bucketCode = GetBucketCode(key.GetHashCode());
            
            int bucketIndex = GetBucketIndex(bucketCode);

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

            int bucketCode = GetBucketCode(key.GetHashCode());
            
            int bucketIndex = GetBucketIndex(bucketCode);

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
            
            return key.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IGenericHashTable<TKey, TValue> Synchronized()
        {
            return new GenericHashTable<TKey, TValue>(isSynchronized: true,
                isReadOnly: this.IsReadOnly,
                initialCapacity: Count,
                isFixedSize: IsFixedSize,
                source: this
            );
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IGenericHashTable<TKey, TValue> AsReadOnly()
        {
            return new GenericHashTable<TKey, TValue>(isSynchronized: false,
                isReadOnly: true,
                initialCapacity: Count,
                isFixedSize: true,
                source: this
            );
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}