/*
      MIT License
     
      Copyright (c) 2025 Alastair Lundy
     
      Permission is hereby granted, free of charge, to any person obtaining a copy
      of this software and associated documentation files (the "Software"), to deal
      in the Software without restriction, including without limitation the rights
      to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
      copies of the Software, and to permit persons to whom the Software is
      furnished to do so, subject to the following conditions:
     
      The above copyright notice and this permission notice shall be included in all
      copies or substantial portions of the Software.
     
      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
      IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
      FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
      AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
      LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
      OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
      SOFTWARE.
   */

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

#if NET8_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif

using System.Linq;

namespace AlastairLundy.DotPrimitives.Collections.Groupings.Concurrent;

/// <summary>
/// A Grouping Collection that is Thread-Safe 
/// </summary>
/// <typeparam name="TKey">The type of the grouping keys.</typeparam>
/// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
public class ConcurrentGroupingCollection<TKey, TElement> : IConcurrentGroupingCollection<TKey, TElement>
{
    private readonly ConcurrentStack<TElement> _stack;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="isReadOnly"></param>
    public ConcurrentGroupingCollection(TKey key, bool isReadOnly = false)
    {
        Key = key;
        IsReadOnly = isReadOnly;
        
        _stack = new ConcurrentStack<TElement>();
        SyncRoot = key is not null ? key : _stack.GetHashCode();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="collection"></param>
    /// <param name="isReadOnly"></param>
    public ConcurrentGroupingCollection(TKey key, ICollection<TElement> collection, bool isReadOnly = false)
    {
        Key = key;
        IsReadOnly = isReadOnly;
        _stack = new ConcurrentStack<TElement>(collection);
        SyncRoot = key is not null ? key : _stack.GetHashCode();
    }

    /// <summary>
    /// Retrieves an enumerator for the internal concurrent collection.
    /// </summary>
    /// <returns>An <see cref="IEnumerator{TElement}"/> object that can be used to iterate through the elements in this concurrent collection.</returns>
    public IEnumerator<TElement> GetEnumerator() => _stack.GetEnumerator();

    /// <summary>
    /// Retrieves an enumerator for the internal concurrent collection.
    /// </summary>
    /// <returns>An <see cref="IEnumerator{TElement}"/> object that can be used to iterate through the elements in this concurrent.</returns>
    IEnumerator IEnumerable.GetEnumerator() => _stack.GetEnumerator();

    /// <summary>
    /// The Key associated with all elements in this Concurrent Grouping Collection.
    /// </summary>
    public TKey Key { get; }

    /// <summary>
    /// Adds an item to this concurrent grouping.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void Add(TElement item)
    {
        _stack.Push(item);
    }

    /// <summary>
    /// Extracts all elements from this grouping collection into an array.
    /// </summary>
    /// <returns>An array containing all elements in this grouping.</returns>
    public TElement[] ToArray() => _stack.ToArray();

    /// <summary>
    /// Removes an item from this concurrent grouping and returns if it was successfully removed.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    /// <returns>A <see cref="bool"/> value that indicates the success of the removal operation.</returns>
    public bool TryTake(
#if NET8_0_OR_GREATER
        [MaybeNullWhen(false)]
#endif
        out TElement item)
    {
       return _stack.TryPop(out item);
    }

    /// <summary>
    /// Removes all items from this concurrent grouping collection.
    /// </summary>
    public void Clear()
    {
        _stack.Clear();
    }

    /// <summary>
    /// Determines whether this concurrent grouping collection contains the specified item.
    /// </summary>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the item was found, false otherwise.</returns>
    public bool Contains(TElement item) => _stack.Contains(item);

    /// <summary>
    /// Removes a specified element from this grouping collection.
    /// </summary>
    /// <remarks>
    /// This method changes the order of the elements in the collection.
    /// </remarks>
    /// <param name="item">The element to be removed.</param>
    /// <returns>true if the element was removed, false otherwise.</returns>
    /// <exception cref="NullReferenceException">Thrown when item is null.</exception>
    public bool Remove(TElement item)
    {
        int index = _stack.IndexOf(item);

        if (index == _stack.Count - 1)
        {
            return _stack.TryPop(out _);
        }
        else
        {
            int numberOfElementsToRemove = (_stack.Count - 1) - index + 1;
            
            IEnumerable<TElement> elementsToReAdd = _stack.Skip(index + 1).Take(numberOfElementsToRemove - 1);

            for (int i = 0; i < numberOfElementsToRemove; i++)
            {
                _stack.TryPop(out _);
            }
            
            _stack.PushRange(elementsToReAdd.ToArray());
            
            return _stack.Contains(item) == false;
        }
    }

    /// <summary>
    /// Copies the elements of this concurrent grouping to the specified array starting at the specified index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the target of the copy.</param>
    /// <param name="index">The zero-based integer indicating the index at which copying begins.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if index is less than 0 or greater than the length of the target array.</exception>
    public void CopyTo(Array array, int index)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(array, nameof(array));
#endif

        if (index < 0 || index > array.Length)
            throw new IndexOutOfRangeException();

        int internalIndex = index;
        foreach (TElement element in _stack)
        {
            array.SetValue(element, internalIndex);
            internalIndex++;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this concurrent grouping collection is synchronized.
    /// </summary>
    public bool IsSynchronized => ((ICollection)_stack).IsSynchronized;

    /// <summary>
    /// 
    /// </summary>
    public object SyncRoot { get; }
    
    /// <summary>
    /// The number of elements in this concurrent grouping collection.
    /// </summary>
    public int Count => _stack.Count;

    /// <summary>
    /// Gets a value indicating whether this collection is read only or not.
    /// </summary>
    public bool IsReadOnly { get; }

    /// <summary>
    /// Copies the elements of this concurrent grouping collection to the specified array.
    /// </summary>
    /// <param name="array">The target array.</param>
    /// <param name="index">The zero-based index at which to begin copying elements.</param>
    /// <exception cref="System.NullReferenceException">Thrown when either the array or index is null.</exception>
    public void CopyTo(TElement[] array, int index) => _stack.CopyTo(array, index);

    /// <summary>
    /// Attempts to add an item to this concurrent grouping.
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <returns>True if the item was successfully added, false otherwise.</returns>
    public bool TryAdd(TElement item)
    {
        lock (_stack)
        {
            Add(item);
        }
        
        TElement? last = _stack.LastOrDefault();

        if (last is not null && last.Equals(item))
            return true;
        
        return _stack.Contains(item);
    }
}