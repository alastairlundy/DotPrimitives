/*
      MIT License
     
      Copyright (c) 2025-2026 Alastair Lundy
     
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

using System.Collections.Concurrent;
using System.Linq;

namespace DotPrimitives.Collections.Groupings.Concurrent;

/// <summary>
/// This interface represents a concurrent grouping collection that is thread-safe and supports concurrent access to a keyed grouping.
/// </summary>
/// <typeparam name="TKey">The type of the grouping keys.</typeparam>
/// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
public interface IConcurrentGroupingCollection<out TKey, TElement> : 
    IProducerConsumerCollection<TElement>,
    IGrouping<TKey,TElement>
{
    /// <summary>
    /// Indicates whether a concurrent grouping collection is read-only.
    /// </summary>
    public bool IsReadOnly { get; }
}