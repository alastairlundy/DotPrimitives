/*
    AlastairLundy.DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System.Collections;
using System.Collections.Generic;

namespace AlastairLundy.DotPrimitives.Collections.Groupings;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TElement"></typeparam>
public class GroupCollection<TKey, TElement> : IGroupingCollection<TKey, TElement>
{
    private readonly ICollection<TElement> _elements;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="elements"></param>
    public GroupCollection(TKey key, IEnumerable<TElement> elements)
    {
        _elements = new List<TElement>(elements);
        Count = _elements.Count;
        Key = key;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="elements"></param>
    public GroupCollection(TKey key, ICollection<TElement> elements)
    {
        Count = elements.Count;
        _elements = elements;
        Key = key;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerator<TElement> GetEnumerator()
    {
        foreach (TElement element in _elements)
        {
           yield return element;
        }
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
    public TKey Key { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public int Count { get; }
}