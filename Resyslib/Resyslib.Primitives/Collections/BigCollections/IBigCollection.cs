namespace AlastairLundy.Resyslib.Primitives.Collections.BigCollections;

public interface IBigCollection<T> : IEnumerable<T>
{
    void Add(T item);
    void Clear();
    
    bool Contains(T item);
    
    bool Remove(T item);
    
    long Count { get; }
    
    bool IsReadOnly { get; }
}