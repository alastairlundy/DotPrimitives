using System.Linq;

namespace AlastairLundy.DotPrimitives.Collections.Groupings;


public interface IGroupingCollection<TKey, TElement> : IGrouping<TKey, TElement>
{
    public int Count { get; }
}