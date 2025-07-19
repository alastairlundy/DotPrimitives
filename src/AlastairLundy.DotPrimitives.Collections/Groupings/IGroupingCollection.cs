/*
    AlastairLundy.DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.DotPrimitives.Collections.Groupings;

/// <summary>
/// Represents a read-only, enumerated sequence of elements grouped by a common key with a count of grouped items.
/// </summary>
/// <typeparam name="TKey">The type of the grouping keys.</typeparam>
/// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
public interface IGroupingCollection<out TKey, TElement> : IGrouping<TKey, TElement>, ICollection<TElement>
{
    /// <summary>
    /// The number of elements in the <see cref="GroupCollection{TKey,TElement}"/>.
    /// </summary>
    public new int Count { get; }
    
    /// <summary>
    /// Whether this <see cref="GroupCollection{TKey,TElement}"/> is read-only or not.
    /// </summary>
    public new bool IsReadOnly { get; }
    
    /// <summary>
    /// Adds an element to the <see cref="GroupCollection{TKey,TElement}"/> if it is not read-only.
    /// </summary>
    /// <param name="element">The element to be added to the <see cref="GroupCollection{TKey,TElement}"/>.</param>
    new void Add(TElement element);
    
    /// <summary>
    /// Removes the first occurrence of the element in the <see cref="GroupCollection{TKey,TElement}"/>.
    /// </summary>
    /// <param name="element">The element to be removed from the <see cref="GroupCollection{TKey,TElement}"/>.</param>
    /// <returns>True if the first occurrence of the element was removed, false otherwise.</returns>
    new bool Remove(TElement element);
    
    /// <summary>
    /// Determines whether the <see cref="GroupCollection{TKey,TElement}"/> contains the specified element.
    /// </summary>
    /// <param name="element">The element to look for.</param>
    /// <returns>True if the element was found in the <see cref="GroupCollection{TKey,TElement}"/>, false otherwise.</returns>
    new bool Contains(TElement element);
    
    /// <summary>
    /// Removes all elements from the <see cref="GroupCollection{TKey,TElement}"/>.
    /// </summary>
    new void Clear();
    
    /// <summary>
    /// Copies each element in the <see cref="GroupCollection{TKey,TElement}"/> to an array, beginning at the specified array index.
    /// </summary>
    /// <param name="array">The array to copy elements to.</param>
    /// <param name="arrayIndex">The index to begin copying elements to in the array.</param>
    new void CopyTo(TElement[] array, int arrayIndex);
}