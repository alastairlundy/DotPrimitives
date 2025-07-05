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

namespace AlastairLundy.DotPrimitives.Collections.Groupings;

/// <summary>
/// A read-only, enumerated collection of elements grouped by a common key.
/// </summary>
/// <typeparam name="TKey">The type of the grouping keys.</typeparam>
/// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
public class GroupByEnumerable<TKey, TElement> : IGrouping<TKey, TElement>
{
    private readonly IEnumerable<TElement> _elements;

    /// <summary>
    /// Instantiates an IEnumerable of elements grouped by a common key.
    /// </summary>
    /// <typeparam name="TKey">The type of the grouping keys.</typeparam>
    /// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
    public GroupByEnumerable(TKey key, IEnumerable<TElement> elements)
    {
        Key = key;
        _elements = elements;
    }

    /// <summary>
    /// Instantiates an IEnumerable of elements grouped by a common key.
    /// </summary>
    /// <typeparam name="TKey">The type of the grouping keys.</typeparam>
    /// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
    /// <returns>The IEnumerable of elements grouped by a common key.</returns>
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
}