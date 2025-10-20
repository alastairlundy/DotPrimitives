using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.DotPrimitives.Collections.Ordered;

public class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>
{
    private IOrderedEnumerable<TElement> _enumerable;
    
    public OrderedEnumerable(IEnumerable<TElement> enumerable)
    {
        _enumerable = _enumerable.
    }
    

    /// <summary>
    /// The key used to group the elements in the Enumerable.
    /// </summary>
    public TKey Key { get; }

    public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector,
        IComparer<TKey> comparer, bool descending)
    {
       
    }

    public IEnumerator<TElement> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}