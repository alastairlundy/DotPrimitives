using System.Linq;

namespace AlastairLundy.DotPrimitives.Collections.Groupings;


/// <summary>
/// Represents a read-only, enumerated sequence of elements grouped by a common key with a count of grouped items.
/// </summary>
/// <typeparam name="TKey">The type of the grouping keys.</typeparam>
/// <typeparam name="TElement">The type of the elements being grouped.</typeparam>
public interface IGroupingCollection<out TKey, out TElement> : IGrouping<TKey, TElement>
{
    /// <summary>
    /// The number of items in the group.
    /// </summary>
    public int Count { get; }
}