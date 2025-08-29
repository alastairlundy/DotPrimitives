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
using System.Collections.Immutable;

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
public class ConcurrentGroupingCollection<TKey, TElement> : IGroupingCollection<TKey, TElement>, IProducerConsumerCollection<TElement>
{
    private ImmutableList<TElement> _list;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="isReadOnly"></param>
    public ConcurrentGroupingCollection(TKey key, bool isReadOnly = false)
    {
        Key = key;
        IsReadOnly = isReadOnly;
        _list = ImmutableList<TElement>.Empty;
        SyncRoot = key is not null ? key : _list.GetHashCode();
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
        _list = ImmutableList.CreateRange(collection);
        SyncRoot = key is not null ? key : _list.GetHashCode();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerator<TElement> GetEnumerator() => _list.GetEnumerator();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

    /// <summary>
    /// 
    /// </summary>
    public TKey Key { get; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public void Add(TElement item)
    {
        _list = _list.Add(item);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public TElement[] ToArray() => _list.ToArray();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool TryTake(
        #if NET8_0_OR_GREATER
[MaybeNullWhen(false)]
 #endif
        out TElement item)
    {
        TElement? removed = _list.Take(1).FirstOrDefault();
        
        _list = ImmutableList.CreateRange(_list.SkipWhile(x => x is not null && x.Equals(removed)));

        if (removed is not null && _list.Contains(removed) == false)
        {
            item = removed;
            return true;
        }
        else
        {
            item = default;
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Clear() => _list = _list.Clear();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Contains(TElement item) => _list.Contains(item);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Remove(TElement item)
    {
      _list = _list.Remove(item);

      return Contains(item) == false;
    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <param name="index"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void CopyTo(Array array, int index)
    {
        #if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(array, nameof(array));
        #endif
        
        if(index < 0 || index > array.Length)
            throw new IndexOutOfRangeException();
        
        for (int i = 0; i < array.Length; ++i)
        {
            array.SetValue(_list[i] , index + i);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool IsSynchronized => ((ICollection)_list).IsSynchronized;
    
    /// <summary>
    /// 
    /// </summary>
    public object SyncRoot { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public int Count => _list.Count;

    /// <summary>
    /// 
    /// </summary>
    public bool IsReadOnly { get; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <param name="index"></param>
    public void CopyTo(TElement[] array, int index) => _list.CopyTo(array, index);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool TryAdd(TElement item)
    {
        Add(item);

        return _list.Contains(item);
    }
}