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

using AlastairLundy.DotPrimitives.Collections.Groupings.Concurrent;

namespace AlastairLundy.DotPrimitives.Collections.Groupings;

public static class AsGroupingExtensions
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupingCollection"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TElement"></typeparam>
    /// <returns></returns>
    public static IGrouping<TKey, TElement> AsGrouping<TKey, TElement>(
        this IGroupingCollection<TKey, TElement> groupingCollection)
    {
        return new GroupingEnumerable<TKey, TElement>(groupingCollection.Key, groupingCollection);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="concurrentGroupingCollection"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TElement"></typeparam>
    /// <returns></returns>
    public static IGrouping<TKey, TElement> AsGrouping<TKey, TElement>(
        this IConcurrentGroupingCollection<TKey, TElement> concurrentGroupingCollection)
    {
        return new GroupingEnumerable<TKey, TElement>(concurrentGroupingCollection.Key, concurrentGroupingCollection);
    }
    
    /// <summary>
    /// Converts a Concurrent grouping collection to a non-concurrent grouping collection.
    /// </summary>
    /// <typeparam name="TKey">The type of the grouping key.</typeparam>
    /// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
    /// <returns>A <see cref="IGroupingCollection{TKey,TElement}"/> collection of the elements grouped by key.</returns>
    public static IGroupingCollection<TKey, TElement> AsGroupingCollection<TKey, TElement>
        (this IConcurrentGroupingCollection<TKey, TElement> concurrentGroupingCollection)
    {
        return new GroupingCollection<TKey, TElement>(concurrentGroupingCollection.Key,
            concurrentGroupingCollection,
            concurrentGroupingCollection.IsReadOnly);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="grouping"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TElement"></typeparam>
    /// <returns></returns>
    public static IGroupingCollection<TKey, TElement> AsGroupingCollection<TKey, TElement>(this
        IGrouping<TKey, TElement> grouping) => new GroupingCollection<TKey, TElement>(grouping.Key, grouping);
}