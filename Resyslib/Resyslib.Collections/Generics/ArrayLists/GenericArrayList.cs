/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

// ReSharper disable ConvertToAutoProperty
// ReSharper disable MergeIntoPattern

// ReSharper disable RedundantBoolCompare
// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.Resyslib.Collections.Generics.ArrayLists
{
    /// <summary>
    /// Like an ArrayList, but uses generics.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericArrayList<T> : IGenericArrayList<T>
    {
        private int _itemsToRemove;
    
        private const int DefaultInitialCapacity = 10;
        private KeyValuePair<T, bool>[] _items;

        private int _capacity;
        private int _count;
    
        private readonly bool _isReadOnly;
        private readonly bool _isFixedSize;
        
        
        /// <summary>
        /// Whether the Generic Array List has a fixed size or not.
        /// </summary>
        public bool IsFixedSize => _isFixedSize;
        
        /// <summary>
        /// Whether the Generic Array List is thread-safe or not.
        /// </summary>
        public bool IsSynchronized { get; protected set; }
        
        /// <summary>
        /// Whether the Generic Array List is read-only or not.
        /// </summary>
        public bool IsReadOnly => _isReadOnly;
        
        /// <summary>
        /// The number of items in the Generic Array List.
        /// </summary>
        public int Count => _count;
        
        /// <summary>
        /// The size of the Generic Array List's internal array.
        /// </summary>
        public int Capacity => _capacity;

        
        /// <summary>
        /// Initializes a new instance of the GenericArrayList class with the specified properties and initial capacity.
        /// </summary>
        /// <param name="isReadOnly">True if the collection is read-only; otherwise, false.</param>
        /// <param name="isFixedSize">True if the collection has a fixed size; otherwise, false.</param>
        /// <param name="isSynchronized">True if access to the collection is thread-safe; otherwise, false.</param>
        /// <param name="capacity">The number of elements in the initial collection.</param>
        protected GenericArrayList(bool isReadOnly, bool isFixedSize, bool isSynchronized, int capacity)
        {
            _capacity = capacity;
            _count = 0;
            _itemsToRemove = 0;
        
            _isReadOnly = isReadOnly;
            _isFixedSize = isFixedSize;
            IsSynchronized = isSynchronized;
            
            _items = new KeyValuePair<T, bool>[capacity];
        }
    
        /// <summary>
        /// Initializes a new instance of the GenericArrayList class with the specified properties and initial capacity.
        /// </summary>
        /// <param name="isReadOnly">True if the collection is read-only; otherwise, false.</param>
        /// <param name="isFixedSize">True if the collection has a fixed size; otherwise, false.</param>
        /// <param name="isSynchronized">True if access to the collection is thread-safe; otherwise, false.</param>
        /// <param name="capacity">The number of elements in the initial collection.</param>
        /// <param name="items">The initial elements of the collection.</param>
        protected GenericArrayList(bool isReadOnly, bool isFixedSize, bool isSynchronized, int capacity, ICollection<T> items)
        {
            _capacity = capacity;
            _count = 0;
            _itemsToRemove = 0;
        
            _isReadOnly = isReadOnly;
            _isFixedSize = isFixedSize;
            IsSynchronized = isSynchronized;

            _items = new KeyValuePair<T, bool>[capacity];
            
            AddRange(items);
        }
    
        /// <summary>
        /// Initializes a new instance of the GenericArrayList class.
        /// </summary>
        public GenericArrayList()
        {
            _items = new KeyValuePair<T, bool>[DefaultInitialCapacity];
            _capacity = DefaultInitialCapacity;
            _count = 0;
            _itemsToRemove = 0;
        
            _isReadOnly = false;
            _isFixedSize = false;
            IsSynchronized = false;
        }

        /// <summary>
        /// Initializes a new instance of the GenericArrayList class with the specified collection.
        /// </summary>
        /// <param name="collection">The initial elements of the collection.</param>
        public GenericArrayList(ICollection<T> collection)
        {
            _items = new KeyValuePair<T, bool>[collection.Count + DefaultInitialCapacity];
            _capacity = collection.Count + DefaultInitialCapacity;
            _itemsToRemove = 0;
        
            _count = collection.Count;
            _isReadOnly = false;
            _isFixedSize = false;
            IsSynchronized = false;
            
            AddRange(collection);
        }
    
        /// <summary>
        /// Initializes a new instance of the GenericArrayList class with the specified capacity.
        /// </summary>
        /// <param name="capacity">The number of elements in the initial collection.</param>
        public GenericArrayList(int capacity)
        {
            _items = new KeyValuePair<T, bool>[capacity];
            _capacity = capacity;
            _itemsToRemove = 0;
            _count = 0;
        
            _isReadOnly = false;
            _isFixedSize = false;
            IsSynchronized = false;
        }

        /// <summary>
        /// Initializes a new instance of the GenericArrayList class with the specified fixed size property.
        /// </summary>
        /// <param name="isFixedSize">True if the collection should have a fixed size; otherwise, false.</param>
        public GenericArrayList(bool isFixedSize)
        {
            IsSynchronized = false;
            _isFixedSize = isFixedSize;
            _items = new KeyValuePair<T, bool>[DefaultInitialCapacity];
            _capacity = DefaultInitialCapacity;
            _itemsToRemove = 0;
            _count = 0;
        
            _isReadOnly = false;
        }

        /// <summary>
        /// Returns an enumerator that allows you to iterate through the collection.
        /// </summary>
        /// <returns>A generic IEnumerator of type T that can be used to traverse the elements of this collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new GenericArrayListEnumerator<T>(this);
        }

        /// <summary>
        /// Returns an enumerator that allows you to iterate through the Generic Array List.
        /// This method is used to provide support for languages that only have IEnumerable, but not IEnumerator.
        /// </summary>
        /// <returns>An IEnumerator that can be used to traverse the elements of this Generic Array List.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Checks if resizing the Generic Array List is required based on the number of items to remove.
        /// </summary>
        private void CheckIfResizeRequired()
        {
            if (_itemsToRemove >= 10)
            {
                TrimToSize();
            }
        }

        /// <summary>
        /// Adds a specified element to the end of the collection.
        /// </summary>
        /// <param name="item">The object to add to the current Generic Array List.</param>
        public void Add(T item)
        {
            if (IsFixedSize)
            {
                return;
            }
        
            if (_capacity > Count)
            {
                _items[Count + 1] = new KeyValuePair<T, bool>(item, false);
                _count++;
            }
            else
            {
                KeyValuePair<T, bool>[] oldItems = new KeyValuePair<T, bool>[Count];
            
                KeyValuePair<T, bool>[] newItems = new KeyValuePair<T, bool>[Count + DefaultInitialCapacity];
            
                Array.Copy(_items, 0, oldItems, 0, Count);

                _items = newItems;
            
                _items[Count + 1] = new KeyValuePair<T, bool>(item, false);
                _count++;
            }
        }

        /// <summary>
        /// Marks all elements in the Generic Array List for removal.
        /// </summary>
        public void Clear()
        {
            RemoveRange(0, Count);

            _capacity = DefaultInitialCapacity;
            
            CheckIfResizeRequired();
        }

        /// <summary>
        /// Determines whether a specified element exists in the collection.
        /// </summary>
        /// <param name="item">The object to be searched for.</param>
        /// <returns>True if the specified element is found, otherwise, false.</returns>
        public bool Contains(T item)
        {
            foreach (KeyValuePair<T, bool> t in _items)
            {
                if (t.Value == false && t.Key != null &&  t.Key.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Copies the contents of the Generic Array List to an Array.
        /// </summary>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">The starting index to copy to in the array.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(0, array, arrayIndex, array.Length);
        }

        /// <summary>
        /// Removes a specified element from the collection.
        /// </summary>
        /// <param name="item">The object to remove from the end of the current list.</param>
        /// <returns>True if the specified element is found and removed; otherwise, false.</returns>
        public bool Remove(T item)
        {
            try
            {
                RemoveAt(IndexOf(item));
                return true;
            }
            catch
            {
                return false;
            }
        }
    
        /// <summary>
        /// Adds a collection of items to the Generic Array List.
        /// </summary>
        /// <param name="collection">The collection to add.</param>
        public void AddRange(ICollection<T> collection)
        {
            if (IsFixedSize)
            {
                return;
            }

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    foreach (T item in collection)
                    {
                        Add(item);
                    }
                }
            }
            else
            {
                foreach (T item in collection)
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Adds an IEnumerable of items to the Generic Array List.
        /// </summary>
        /// <param name="enumerable">The IEnumerable to add.</param>
        public void AddRange(IEnumerable<T> enumerable)
        {
            if (IsFixedSize)
            {
                return;
            }

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    foreach (T item in enumerable)
                    {
                        Add(item);
                    }
                }
            }
            else
            {
                foreach (T item in enumerable)
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Performs a binary search on the Generic Array List.
        /// </summary>
        /// <param name="index">The starting index of the range to search for.</param>
        /// <param name="count">The length of the range to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="comparer">The comparer implementation to use.</param>
        /// <returns>The zero-based index of the item if found; -1 otherwise.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than 0 or if the index is greater than the number of items.</exception>
        public int BinarySearch(int index, int count, T value, IComparer<T> comparer)
        {
            if (Count != Capacity)
            {
                TrimToSize();
            }
            
            Sort();

            if (index < 0 || index >= int.MaxValue | index >= Count || count < 0 || count > Count)
            {
                throw new IndexOutOfRangeException();
            }

            return Array.BinarySearch(_items, index, count, value);
        }
        
        /// <summary>
        /// Performs a binary search on the Generic Array List.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <param name="comparer">The comparer implementation to use.</param>
        /// <returns>The zero-based index of the item if found; -1 otherwise.</returns>
        public int BinarySearch(T value, IComparer<T> comparer)
        {
            if (Count != Capacity)
            {
                TrimToSize();
            }
            
            Sort();

            return Array.BinarySearch(_items, value, (IComparer)comparer);
        }

        /// <summary>
        /// Performs a binary search on the Generic Array List.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>The zero-based index of the item if found; -1 otherwise.</returns>
        public int BinarySearch(T value)
        {
            if (Count != Capacity)
            {
                TrimToSize();
            }
            
            Sort();
            
            return Array.BinarySearch(_items, value);
        }

        /// <summary>
        /// Copies the contents of the Generic Array List to an Array.
        /// </summary>
        /// <param name="array">The array to copy to.</param>
        public void CopyTo(T[] array)
        {
           CopyTo(0, array, 0, array.Length);
        }

        /// <summary>
        /// Copies a specified number of items from one Generic Array List from to an Array 
        /// </summary>
        /// <param name="index">The starting index of the array to copy to.</param>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">The starting index to copy to in the array.</param>
        /// <param name="count">The number of items to copy to the array.</param>
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            if (index > Count || index < 0 || count < 1 || count > Count)
            {
                throw new IndexOutOfRangeException();
            }
            
            CheckIfResizeRequired();
            
            int limit = index + count;

            for (int i = index; i < limit; i++)
            {
                if (_items[i].Value == true)
                {
                    if (limit < Count)
                    {
                        limit++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    T item = _items[i].Key;
                    array[i] = item;
                }
            }
        }

        /// <summary>
        /// Creates a Fixed Size Generic Array List from a source Generic Array List.
        /// </summary>
        /// <param name="source">The source to make a fixed size copy of.</param>
        /// <returns>The fixed size version of the source.</returns>
        [Pure]
        public IGenericArrayList<T> FixedSize(IGenericArrayList<T> source)
        {
            return new GenericArrayList<T>(source.IsReadOnly, true, source.IsSynchronized, source.Count, source);
        }
        
        /// <summary>
        /// Creates a Fixed Size List from a source IList.
        /// </summary>
        /// <param name="source">The source to make a fixed size copy of.</param>
        /// <returns>The fixed size version of the source.</returns>
        [Pure]
        public IList<T> FixedSize(IList<T> source)
        {
            return new GenericArrayList<T>(source.IsReadOnly, true, false, source.Count, source);
        }
        
        /// <summary>
        /// Creates a Generic Array List from a range of items in the current Generic Array List.
        /// </summary>
        /// <param name="index">The index to start copying items from.</param>
        /// <param name="count">The number of items to copy.</param>
        /// <returns>The new Generic Array List with the copied range of items.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified count is more than the number of items in the Generic Array List.</exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        [Pure]
        public IGenericArrayList<T> GetRange(int index, int count)
        {
            if (count > Count || count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if (index > Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            
            GenericArrayList<T> list = new GenericArrayList<T>();

            int  limit = index + count;

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    for (int i = index; i < limit; i++)
                    {
                        if (limit >= Count)
                        {
                            limit = Count;
                        }

                        if (_items[i].Value == true)
                        {
                            list.Add(_items[i].Key);
                        }
                        else
                        {
                            limit++;
                        }
                    }
                }
            }
            else
            {
                for (int i = index; i < limit; i++)
                {
                    if (limit >= Count)
                    {
                        limit = Count;
                    }
                
                    if (_items[i].Value == true)
                    {
                        list.Add(_items[i].Key);       
                    }
                    else
                    {
                        limit++;
                    }
                }
            }
            
            return list;
        }

        /// <summary>
        /// Gets the index of the specified value.
        /// </summary>
        /// <param name="value">The item to be found.</param>
        /// <param name="startIndex">The index to start looking for the item at.</param>
        /// <returns>The index of the item.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index is greater than the number of items in the collection or if the start index is less than 0.</exception>
        public int IndexOf(T? value, int startIndex)
        {
            if (startIndex > Count || startIndex < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    for (int index = startIndex; index < Count; index++)
                    {
                        KeyValuePair<T, bool> pair = _items[index];
            
                        if (pair.Value == false && pair.Key is not null && pair.Key.Equals(value))
                        {
                            return index;
                        }
                    }
                }
            }
            else
            {
                for (int index = startIndex; index < Count; index++)
                {
                    KeyValuePair<T, bool> pair = _items[index];
            
                    if (pair.Value == false && pair.Key is not null && pair.Key.Equals(value))
                    {
                        return index;
                    }
                }
            }
        
            return -1;
        }

        /// <summary>
        /// Gets the index of the specified value.
        /// </summary>
        /// <param name="value">The item to be found.</param>
        /// <param name="startIndex">The index to start looking for the item at.</param>
        /// <param name="count">The number of items to look at to check the index.</param>
        /// <returns>The index of the item if found; -1 otherwise.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index or count are greater than the number of items in the collection or if the start index or count are less than 0.</exception>
        public int IndexOf(T? value, int startIndex, int count)
        {
            int index = -1;
            
            if (startIndex > Count || startIndex < 0 || count < 1 || count > Count)
            {
                throw new IndexOutOfRangeException();
            }

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    for (int i = startIndex; i < Count + count; i++)
                    {
                        T key = _items[i].Key;
                
                        if (key is not null && key.Equals(value))
                        {
                            index = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = startIndex; i < Count + count; i++)
                {
                    T key = _items[i].Key;
                
                    if (key is not null && key.Equals(value))
                    {
                        index = i;
                        break;
                    }
                }
            }

            return index;
        }

        /// <summary>
        /// Inserts a range of items starting a specific index of the collection.
        /// </summary>
        /// <param name="index">The index to start inserting items from.</param>
        /// <param name="collection">The collection to insert items from.</param>
        public void InsertRange(int index, ICollection<T> collection)
        {
            if (index > Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    foreach (T item in collection)
                    {
                        Insert(index, item);
                    }
                }
            }
            else
            {
                foreach (T item in collection)
                {
                    Insert(index, item);
                }
            }
        }

        /// <summary>
        /// Finds the last index of the value.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>The last index of the value if found; -1 otherwise.</returns>
        public int LastIndexOf(T value)
        {
            return LastIndexOf(value, _items.Length - 1);
        }

        /// <summary>
        /// Finds the last index of the value.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <param name="startIndex">The index to start searching for the value.</param>
        /// <returns>The last index of the value if found; -1 otherwise.</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public int LastIndexOf(T value, int startIndex)
        {
            int lastIndex = -1;
            
            if (startIndex > Count || startIndex < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    for (int index = startIndex; _items.Length > startIndex; index--)
                    {
                        KeyValuePair<T, bool> item = _items[index];

                        if (item.Value == false && item.Key != null && item.Key.Equals(value))
                        {
                            if (index > lastIndex)
                            {
                                lastIndex = index;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int index = startIndex; _items.Length > startIndex; index--)
                {
                    KeyValuePair<T, bool> item = _items[index];

                    if (item.Value == false && item.Key != null && item.Key.Equals(value))
                    {
                        if (index > lastIndex)
                        {
                            lastIndex = index;
                        }
                    }
                }   
            }

            return lastIndex;
        }

        /// <summary>
        /// Finds the last index of the value.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <param name="startIndex">The index to start searching for the value.</param>
        /// <param name="count">The number of items to remove from the Generic Array List.</param>
        /// <returns>The last index of the value if found; -1 otherwise.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index or count are greater than the number of items in the collection or if the start index or count are less than 0.</exception>
        public int LastIndexOf(T value, int startIndex, int count)
        {
            if (startIndex > Count || startIndex < 0 || count < 1 || count > Count)
            {
                throw new IndexOutOfRangeException();
            }

            int limit = startIndex + count + 1;
            int lastIndex = -1;

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    for (int i = startIndex; i < limit; i++)
                    {
                        if (_items[i].Value == false && limit < Count)
                        {
                            limit++;
                        }
                        else if (limit >= Count)
                        {
                            limit = Count;
                        }
                        else if (i >= Count)
                        {
                            break;
                        }

                        if (_items[i].Value == true)
                        {
                            T key = _items[i].Key;
                    
                            if (key is not null && key.Equals(value))
                            {
                                lastIndex = i;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = startIndex; i < limit; i++)
                {
                    if (_items[i].Value == false && limit < Count)
                    {
                        limit++;
                    }
                    else if (limit >= Count)
                    {
                        limit = Count;
                    }
                    else if (i >= Count)
                    {
                        break;
                    }

                    if (_items[i].Value == true)
                    {
                        T key = _items[i].Key;
                    
                        if (key is not null && key.Equals(value))
                        {
                            lastIndex = i;
                        }
                    }
                }
            }
            
            return lastIndex;
        }

        /// <summary>
        /// Creates a read-only copy of a Generic Array List.
        /// </summary>
        /// <param name="source">The source to make a read-only copy of.</param>
        /// <returns>A read-only copy of the Generic Array List.</returns>
        [Pure]
        public IGenericArrayList<T> ReadOnly(IGenericArrayList<T> source)
        {
            return new GenericArrayList<T>(true, source.IsFixedSize, source.IsSynchronized, source.Capacity, source);
        }

        /// <summary>
        /// Creates a read-only copy of an IList.
        /// </summary>
        /// <param name="source">The source to make a read-only copy of.</param>
        /// <returns>A read-only copy of the IList.</returns>
        [Pure]
        public IList<T> ReadOnly(IList<T> source)
        {
            bool isFixedSize;
            bool isSynchronized;

            if (source is T[] array)
            {
                isFixedSize = true;
                isSynchronized = array.IsSynchronized;
            }
            else
            {
                isFixedSize = true;
                isSynchronized = false;
            }
            
            return new GenericArrayList<T>(true, isFixedSize, isSynchronized, source.Count, source);
        }

        /// <summary>
        /// Removes a range of items starting from a specified index.
        /// </summary>
        /// <param name="index">The index to start removing items from.</param>
        /// <param name="count">The number of items to remove from the Generic Array List.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index or count are greater than the number of items in the collection or if the start index is less than 0 or the count is less than 1.</exception>
        public void RemoveRange(int index, int count)
        {
            if (index > Count || index < 0 || count < 1 || count > Count)
            {
                throw new IndexOutOfRangeException();
            }

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    for (int i = 0; i < count; i++)
                    {
                        RemoveAt(index);
                    }
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    RemoveAt(index);
                }
            }
        }
        
        /// <summary>
        /// Creates a new Generic Array List with a specified number of copies of a value.
        /// </summary>
        /// <param name="value">The value to fill the new Generic Array List in.</param>
        /// <param name="count">The number of copied values to add to the new Generic Array List.</param>
        /// <returns>The new Generic Array List with the copies of the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the start count is greater than the number of items in the Generic Array List or if the count is less than 0.</exception>
        [Pure]
        public IGenericArrayList<T> Repeat(T value, int count)
        {
            GenericArrayList<T> list = new();

            if (count < 0 || count >= int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    for (int i = 0; i < count; i++)
                    {
                        list.Add(value);
                    }
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    list.Add(value);
                }
            }
            
            return list;
        }
     
        /// <summary>
        /// Reverses the items in the Generic Array List.
        /// </summary>
        public void Reverse()
        {
            KeyValuePair<T, bool>[] newItems = new KeyValuePair<T, bool>[Count];

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        if (Count - (i + 1) >= 0)
                        {
                            newItems[Count - (i + 1)] = _items[i];
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    if (Count - (i + 1) >= 0)
                    {
                        newItems[Count - (i + 1)] = _items[i];
                    }
                }
            }
            
            Array.Copy(newItems, 0, _items, 0, Count);
        }

        /// <summary>
        /// Reverses the selected items in the Generic Array List.
        /// </summary>
        /// <param name="index">The index to start reversing the selected items.</param>
        /// <param name="count">The number of items to reverse.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index or count are greater than the number of items in the collection or if the start index or count are less than 0.</exception>
        public void Reverse(int index, int count)
        {
            if (index > Count || index < 0 || count < 1 || count > Count)
            {
                throw new IndexOutOfRangeException();
            }

            List<KeyValuePair<T, bool>> newItems = new();

            int reversedCount = 0;

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        if (i < index || i > (index + count))
                        {
                            newItems.Add(_items[i]);
                        }
                        else if(i >= index && i <= index + count && reversedCount <= count)
                        {
                            newItems.Insert(i, _items[(index + count) - reversedCount]);
                    
                            reversedCount++;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    if (i < index || i > (index + count))
                    {
                        newItems.Add(_items[i]);
                    }
                    else if(i >= index && i <= index + count && reversedCount <= count)
                    {
                        newItems.Insert(i, _items[(index + count) - reversedCount]);
                    
                        reversedCount++;
                    }
                }
            }
            
            Array.Copy(newItems.ToArray(), 0, _items, 0, Count);
        }

        /// <summary>
        /// Sets a collection of objects to positions in the Generic Array List from the starting index.
        /// </summary>
        /// <param name="index">The index to set the objects from.</param>
        /// <param name="collection">The collection of items to set to positions in the Generic Array List.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is greater than the number of items in the collection or if the start index is less than 0.</exception>
        public void SetRange(int index, ICollection<T> collection)
        {
            if (index > Count || index < 0 || collection.Count < 1)
            {
                throw new IndexOutOfRangeException();
            }

            int i = index;

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    foreach (T item in collection)
                    {
                        Insert(i, item);
                        i++;
                    }
                }
            }
            else
            {
                foreach (T item in collection)
                {
                    Insert(i, item);
                    i++;
                }
            }
        }

        /// <summary>
        /// Sets a collection of objects to positions in the Generic Array List from the starting index.
        /// </summary>
        /// <param name="index">The index to set the objects from.</param>
        /// <param name="enumerable">The collection of items to set to positions in the Generic Array List.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index or count are greater than the number of items in the collection or if the start index is less than 0 or the count is less than 1.</exception>
        public void SetRange(int index, IEnumerable<T> enumerable)
        {
            IList<T> array;
            
            if (enumerable is IList<T> list)
            {
                array = list;
            }
            else
            {
               array = enumerable.ToArray();
            }
           
            
            if (index > Count || index < 0 || array.Count < 1 || array.Count > Count)
            {
                throw new IndexOutOfRangeException();
            }

            int i = index;

            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    foreach (T item in array)
                    {
                        Insert(i, item);
                        i++;
                    }
                }
            }
            else
            {
                foreach (T item in array)
                {
                    Insert(i, item);
                    i++;
                }
            }
        }

        /// <summary>
        /// Sorts the Generic Array List by calling the internal Array's sort method.
        /// </summary>
        public void Sort()
        {
            Array.Sort(_items);
        }

        /// <summary>
        /// Sorts the Generic Array List using an IComparer.
        /// </summary>
        /// <param name="comparer">The comparer implementation to use.</param>
        public void Sort(IComparer<KeyValuePair<T, bool>> comparer)
        {
            Array.Sort(_items, comparer);   
        }
        
        /// <summary>
        /// Sorts the Generic Array List using an IComparer.
        /// </summary>
        /// <param name="index">The index to start sorting from.</param>
        /// <param name="count">The number of items to sort.</param>
        /// <param name="comparer">The comparer implementation to use.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index or count are greater than the number of items in the collection or if the start index is less than 0 or the count is less than 1.</exception>
        public void Sort(int index, int count, IComparer<KeyValuePair<T, bool>> comparer)
        {
            if (index > Count || index < 0 || count < 1 || count > Count)
            {
                throw new IndexOutOfRangeException();
            }
            
            Array.Sort(_items, index, count, comparer);
        }

        /// <summary>
        /// Creates a thread-safe copy of the source Generic Array List.
        /// </summary>
        /// <param name="source">The source to make a thread-safe copy of.</param>
        /// <returns>A thread-safe copy of the Generic Array List.</returns>
        [Pure]
        public IGenericArrayList<T> Synchronized(IGenericArrayList<T> source)
        {
            return new GenericArrayList<T>(source.IsReadOnly, source.IsFixedSize, true, source.Capacity, source);
        }

        /// <summary>
        /// Creates a thread-safe copy of the source IList.
        /// </summary>
        /// <param name="source">The source to make a thread-safe copy of.</param>
        /// <returns>A thread-safe copy of the IList.</returns>
        [Pure]
        public IList<T> Synchronized(IList<T> source)
        {
            bool isFixedSize = source is T[];

            return new GenericArrayList<T>(source.IsReadOnly, isFixedSize, true, source.Count, source); 
        }

    
        /// <summary>
        /// Returns the items in the Generic Array List as an array. 
        /// </summary>
        /// <returns>The array with the contents of the Generic Array List.</returns>
        public T[] ToArray()
        {
            TrimToSize();
            
            T[] array = new T[Count];

            for (int i = 0; i < Count; i++)
            {
                array[i] = _items[i].Key;
            }

            return array;
        }

        /// <summary>
        /// Trims the internal array, used by the Generic Array List, to the number of items in the array.
        /// </summary>
        public void TrimToSize()
        {
            Array.Resize(ref _items, _capacity);
            _itemsToRemove = 0;
        }

        /// <summary>
        /// Gets the index of the specified value.
        /// </summary>
        /// <param name="item">The item to be found.</param>
        /// <returns>The index of the item if found; -1 otherwise.</returns>
        public int IndexOf(T item)
        {
            if (IsSynchronized)
            {
                lock (_items.SyncRoot)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        T key = _items[i].Key;
                
                        if (key is not null && key.Equals(item))
                        {
                            return i;
                        }
                    }
                }   
            }
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    T key = _items[i].Key;
                
                    if (key is not null && key.Equals(item))
                    {
                        return i;
                    }
                }
            }
        
            return -1;
        }

        /// <summary>
        /// Inserts an item into the Generic Array List at the specified index.
        /// </summary>
        /// <param name="index">The index to insert the item at.</param>
        /// <param name="item">The item to be added.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than 0 or if the Generic Array List cannot have more items added to it.</exception>
        public void Insert(int index, T item)
        {
            if (index >= int.MaxValue || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            
            if (index > Capacity && index == Count + 1)
            {
                Add(item);
            }
            else if(index < Capacity)
            {
                ShiftItemsUpOne(index);
                
                _items[index] = new KeyValuePair<T, bool>(item, false);
            }
        }

        private void ShiftItemsUpOne(int indexStart)
        {
            if (indexStart > Count || indexStart < 0)
            {
                throw new IndexOutOfRangeException();
            }
            
#if NET5_0_OR_GREATER
            if (Count < Array.MaxLength)
#else
            if (Count < int.MaxValue)
#endif
            {
                if (IsSynchronized)
                {
                    lock (_items.SyncRoot)
                    {
                        for (int i = Count + 1; i >= indexStart ; i--)
                        {
                            _items[i] = _items[i - 1];
                        }
                    }
                }
                else
                {
                    for (int i = Count + 1; i >= indexStart ; i--)
                    {
                        _items[i] = _items[i - 1];
                    }
                }
            }
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The index of the item to be removed.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than 0 or if the index is greater than the number of items in the Generic Array List.</exception>
        public void RemoveAt(int index)
        {
            if (index > Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            
            _items[index] = new KeyValuePair<T, bool>(_items[index].Key, false);

            
            CheckIfResizeRequired();
        }

        /// <summary>
        /// The item at the specified index within the Generic Array List.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        public T this[int index]
        {
            get
            {
                if (index > Count || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                
                if (_items[index].Value == true)
                {
                    int i = index;

                    if (IsSynchronized)
                    {
                        lock (_items.SyncRoot)
                        {
                            while (i < Count)
                            {
                                if (_items[i].Value == false)
                                {
                                    return _items[i].Key;
                                }

                                i++;
                            }
                        }
                    }
                    else
                    {
                        while (i < Count)
                        {
                            if (_items[i].Value == false)
                            {
                                return _items[i].Key;
                            }

                            i++;
                        }
                    }
                
                    return _items[i].Key;
                }
                else
                {
                    return _items[index].Key;   
                }
            }
            set => _items[index] = new KeyValuePair<T, bool>(value, false);
        }

        /// <summary>
        /// Returns a shallow copy of this Generic Array List.
        /// </summary>
        /// <returns>A shallow copy of this Generic Array List as an object.</returns>
        public object Clone()
        {
            return this;
        }
    }
}