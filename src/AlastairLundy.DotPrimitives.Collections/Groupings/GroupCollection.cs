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
/// A read-only, enumerated collection of elements grouped by a common key.
/// </summary>
/// <typeparam name="TKey">The type of the grouping keys.</typeparam>
/// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
public class GroupCollection<TKey, TElement> : IGroupingCollection<TKey, TElement>
{
    private readonly ICollection<TElement> _elements;

    /// <summary>
    /// Instantiates a collection of grouped by a common key.
    /// </summary>
    /// <param name="key">The key to group elements by.</param>
    /// <param name="elements">The sequence of elements to group.</param>
    public GroupCollection(TKey key, IEnumerable<TElement> elements)
    {
        _elements = new List<TElement>(elements);
        Count = _elements.Count;
        Key = key;
    }
    
    /// <summary>
    /// Instantiates a collection of grouped by a common key.
    /// </summary>
    /// <param name="key">The key to group elements by.</param>
    /// <param name="elements">The collection of elements to group.</param>
    public GroupCollection(TKey key, ICollection<TElement> elements)
    {
        Count = elements.Count;
        _elements = elements;
        Key = key;
    }
    
    /// <summary>
    /// Instantiates a collection of elements grouped by a common key.
    /// </summary>
    /// <typeparam name="TKey">The type of the grouping keys.</typeparam>
    /// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
    /// <returns>The collection of elements grouped by a common key.</returns>
    public IEnumerator<TElement> GetEnumerator()
    {
        foreach (TElement element in _elements)
        {
           yield return element;
        }
    }

    /// <summary>
    /// Returns an enumerator for the elements in this grouping, which enumerates each element individually.
    /// </summary>
    /// <returns>An enumerator that yields each element in the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// The key used to group the elements in the Enumerable.
    /// </summary>
    public TKey Key { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public int Count { get; }
}