/*
    AlastairLundy.DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace AlastairLundy.DotPrimitives.Collections.Groupings;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey">The type of the grouping keys.</typeparam>
/// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
public class GroupingList<TKey, TElement> : IGroupingList<TKey, TElement>
{
    private readonly List<TElement> _elements;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="isReadOnly"></param>
    public GroupingList(TKey key, bool isReadOnly = false)
    {
        _elements = new  List<TElement>();
        IsReadOnly = isReadOnly;
        Key = key;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="elements"></param>
    /// <param name="isReadOnly"></param>
    public GroupingList(TKey key, IEnumerable<TElement> elements, bool isReadOnly = false)
    {
          Key = key;
          IsReadOnly = isReadOnly;
          _elements = new  List<TElement>(elements);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerator<TElement> GetEnumerator() => _elements.GetEnumerator();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public TKey Key { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public void Add(TElement item)
    {
        if (IsReadOnly)
            throw new NotSupportedException();
            
        _elements.Add(item);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public bool Contains(TElement element) => _elements.Contains(element);

    /// <summary>
    /// 
    /// </summary>
    public void Clear() => _elements.Clear();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void CopyTo(TElement[] array, int arrayIndex)
    {
        #if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(array);
        #endif

        if (arrayIndex < 0 || arrayIndex > array.Length)
            throw new IndexOutOfRangeException();
        
        _elements.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Remove(TElement item)
    {
        if (IsReadOnly)
            throw new NotSupportedException();
        
        return _elements.Remove(item);
    }

    /// <summary>
    /// 
    /// </summary>
    public int Count => _elements.Count;
    
    /// <summary>
    /// 
    /// </summary>
    public bool IsReadOnly { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int IndexOf(TElement item) => _elements.IndexOf(item);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="item"></param>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void Insert(int index, TElement item)
    {
        if (IsReadOnly)
            throw new NotSupportedException();

        if (index < 0 || index > Count)
            throw new IndexOutOfRangeException();
            
        _elements.Insert(index, item);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void RemoveAt(int index)
    {
        if (IsReadOnly)
            throw new NotSupportedException();
        
        if(index < 0 || index > Count)
            throw new IndexOutOfRangeException();
        
        _elements.RemoveAt(index);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public TElement this[int index]
    {
        get
        {
            if (index < 0 || index >= _elements.Count)
                throw new IndexOutOfRangeException();
            
            return _elements[index];
        }
        set
        {
            if (index < 0 || index >= _elements.Count)
                throw new IndexOutOfRangeException();
            
            _elements[index] = value;
        }
    }
}