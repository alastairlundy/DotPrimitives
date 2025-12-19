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

using System.Linq;

namespace DotPrimitives.Collections.Groupings;

/// <summary>
/// A read-only, enumerated sequence of elements grouped by a common key.
/// </summary>
/// <typeparam name="TKey">The type of the grouping keys.</typeparam>
/// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
public class GroupingEnumerable<TKey, TElement> : IGrouping<TKey, TElement>
{
    private readonly IEnumerable<TElement> _elements;

    /// <summary>
    /// Instantiates an IEnumerable of elements grouped by a common key.
    /// </summary>
    /// <typeparam name="TKey">The type of the grouping keys.</typeparam>
    /// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
    /// <param name="key">The key to group elements by.</param>
    /// <param name="elements">The sequence of elements to group.</param>
    public GroupingEnumerable(TKey key, IEnumerable<TElement> elements)
    {
        Key = key;
        _elements = new List<TElement>(elements);
    }

    /// <summary>
    /// Instantiates an IEnumerable of elements grouped by a common key.
    /// </summary>
    /// <typeparam name="TKey">The type of the grouping keys.</typeparam>
    /// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
    /// <returns>The IEnumerable of elements grouped by a common key.</returns>
    public IEnumerator<TElement> GetEnumerator() => _elements.GetEnumerator();

    /// <summary>
    /// Returns an enumerator for the elements in this grouping, which enumerates each element individually.
    /// </summary>
    /// <returns>An enumerator that yields each element in the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// The key used to group the elements in the Enumerable.
    /// </summary>
    public TKey Key { get; }
}