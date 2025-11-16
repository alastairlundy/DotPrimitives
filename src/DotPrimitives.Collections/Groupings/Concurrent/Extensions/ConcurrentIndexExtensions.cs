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

using System.Collections.Concurrent;

namespace AlastairLundy.DotPrimitives.Collections.Groupings.Concurrent;

public static class ConcurrentIndexExtensions
{
    /// <param name="collection">The concurrent grouping collection to search.</param>
    /// <typeparam name="TKey">The type of the grouping keys.</typeparam>
    /// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
    extension<TKey, TElement>(IConcurrentGroupingCollection<TKey, TElement> collection)
    {
        /// <summary>
        /// Finds the first index of the specified element in a concurrent grouping collection.
        /// </summary>
        /// <param name="element">The element to get the index of.</param>
        /// <returns>The index of the element in the collection, or -1 if not found.</returns>
        public int IndexOf(TElement element)
        {
            int index = 0;

            foreach (TElement item in collection)
            {
                if (item is not null && item.Equals(element))
                    return index;
            
                index++;
            }

            return -1;
        }
    }

    /// <param name="concurrentCollection"></param>
    /// <typeparam name="TElement"></typeparam>
    extension<TElement>(IProducerConsumerCollection<TElement> concurrentCollection)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        internal int IndexOf(TElement element)
        {
            int index = 0;

            foreach (TElement item in concurrentCollection)
            {
                if (item is not null && item.Equals(element))
                    return index;
            
                index++;
            }

            return -1;
        }
    }
}