/*
    AlastairLundy.DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.DotPrimitives.Collections.Generics.Groupings;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TElement"></typeparam>
public class GroupByEnumerable<TKey, TElement> : IGrouping<TKey, TElement>
{
    private readonly IEnumerable<TElement> _elements;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="elements"></param>
    public GroupByEnumerable(TKey key, IEnumerable<TElement> elements)
    {
        Key = key;
        _elements = elements;
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
}