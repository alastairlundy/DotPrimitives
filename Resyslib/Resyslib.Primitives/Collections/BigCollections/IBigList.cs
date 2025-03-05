namespace AlastairLundy.Resyslib.Primitives.Collections.BigCollections;

public interface IBigList<T> : IBigCollection<T>
{
    T this[long index] { get; }
    
    long IndexOf(T item);
    
    void Insert(long index, T item);
    void RemoveAt(long index);
}