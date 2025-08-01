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
/// A read-only, enumerated collection of elements grouped by a common key.
/// </summary>
/// <typeparam name="TKey">The type of the grouping keys.</typeparam>
/// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
public class GroupingCollection<TKey, TElement> : IGroupingCollection<TKey, TElement>
{
    private readonly ICollection<TElement> _elements;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="isReadOnly"></param>
    public GroupingCollection(TKey key, bool isReadOnly = false)
    {
        Key = key;
        IsReadOnly = isReadOnly;
        _elements = new List<TElement>();
    }
    
    /// <summary>
    /// Instantiates a collection of grouped by a common key.
    /// </summary>
    /// <param name="key">The key to group elements by.</param>
    /// <param name="elements">The sequence of elements to group.</param>
    /// <param name="isReadOnly">Whether the GroupCollection is read-only or not.</param>
    public GroupingCollection(TKey key, IEnumerable<TElement> elements, bool isReadOnly = false)
    {
        _elements = new List<TElement>(elements);
        Key = key;
        IsReadOnly = isReadOnly;
    }

    /// <summary>
    /// Instantiates a collection of grouped by a common key.
    /// </summary>
    /// <param name="key">The key to group elements by.</param>
    /// <param name="elements">The collection of elements to group.</param>
    /// <param name="isReadOnly">Whether the GroupCollection is read-only or not.</param>
    public GroupingCollection(TKey key, ICollection<TElement> elements, bool isReadOnly = false)
    {
        _elements = elements;
        Key = key;
        IsReadOnly = isReadOnly;
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
    /// The key used to group the elements in the <see cref="GroupingCollection{TKey,TElement}"/>.
    /// </summary>
    public TKey Key { get; }
    
    /// <summary>
    /// The number of elements in the <see cref="GroupingCollection{TKey,TElement}"/>.
    /// </summary>
    public int Count => _elements.Count;

    /// <summary>
    /// Whether this <see cref="GroupingCollection{TKey,TElement}"/> is read-only or not.
    /// </summary>
    public bool IsReadOnly { get; }

    /// <summary>
    /// Adds an element to the <see cref="GroupingCollection{TKey,TElement}"/> if it is not read-only.
    /// </summary>
    /// <param name="element">The element to be added to the <see cref="GroupingCollection{TKey,TElement}"/>.</param>
    /// <exception cref="NotSupportedException">Thrown if the <see cref="GroupingCollection{TKey,TElement}"/> is read-only.</exception>
    public void Add(TElement element)
    {
        _elements.Add(element);
    }

    /// <summary>
    /// Removes the first occurrence of the element in the <see cref="GroupingCollection{TKey,TElement}"/>.
    /// </summary>
    /// <param name="element">The element to be removed from the <see cref="GroupingCollection{TKey,TElement}"/>.</param>
    /// <returns>True if the first occurrence of the element was removed, false otherwise.</returns>
    /// <exception cref="NotSupportedException">Thrown if the <see cref="GroupingCollection{TKey,TElement}"/> is read-only.</exception>
    public bool Remove(TElement element)
    {
        return _elements.Remove(element);
    }

    /// <summary>
    /// Determines whether the <see cref="GroupingCollection{TKey,TElement}"/> contains the specified element.
    /// </summary>
    /// <param name="element">The element to look for.</param>
    /// <returns>True if the element was found in the <see cref="GroupingCollection{TKey,TElement}"/>, false otherwise.</returns>
    public bool Contains(TElement element)
    {
        return _elements.Contains(element);
    }

    /// <summary>
    /// Removes all elements from the <see cref="GroupingCollection{TKey,TElement}"/>.
    /// </summary>
    public void Clear()
    {
        _elements.Clear();
    }
    public void Clear() => _elements.Clear();

    /// <summary>
    /// Copies each element in the <see cref="GroupingCollection{TKey,TElement}"/> to an array, beginning at the specified array index.
    /// </summary>
    /// <param name="array">The array to copy elements to.</param>
    /// <param name="arrayIndex">The index to begin copying elements to in the array.</param>
    public void CopyTo(TElement[] array, int arrayIndex)
    {
        _elements.CopyTo(array, arrayIndex);
    }
}