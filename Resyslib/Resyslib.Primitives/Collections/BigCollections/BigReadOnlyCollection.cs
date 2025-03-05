using System.Collections;

namespace AlastairLundy.Resyslib.Primitives.Collections.BigCollections;

public class BigReadOnlyCollection<T> : IBigCollection<T>
{
    
    
    public IEnumerator<T> GetEnumerator()
    {
        
    }

    public void Add(T item)
    {
        
    }

    public void Clear()
    {
        
    }

    public bool Contains(T item)
    {
        
    }

    public bool Remove(T item)
    {
        
    }

    public long Count { get; }
    public bool IsReadOnly { get; }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}