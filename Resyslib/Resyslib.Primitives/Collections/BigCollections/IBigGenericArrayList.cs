namespace AlastairLundy.Resyslib.Primitives.Collections.BigCollections;

public interface IBigGenericArrayList<T> : IBigList<T>, ICloneable
{
    bool IsFixedSize { get; }

    bool IsSynchronized { get; }
    
    long Capacity { get; }
    
    void AddRange(ICollection<T> collection);
     
    long BinarySearch(long index, long count, T value, IComparer<T> comparer); 
    long BinarySearch(T value, IComparer<T> comparer);

    void CopyTo(T[] array);
    void CopyTo(long index, T[] array, long arrayIndex, long count);
     
    IBigGenericArrayList<T> FixedSize(IBigGenericArrayList<T> source);
    IBigList<T> FixedSize(IBigList<T> source);
     
    IBigGenericArrayList<T> GetRange(long index, long count);

    long IndexOf(T? value, long startIndex);
    long IndexOf(T? value, long startIndex, long count);

    void InsertRange(long index, ICollection<T> collection);
     
    long LastIndexOf(T value);
    long LastIndexOf(T value, long startIndex);
    long LastIndexOf(T value, long startIndex, long count);
     
    IBigGenericArrayList<T> ReadOnly(IBigGenericArrayList<T> source);
    IBigList<T> ReadOnly(IBigList<T> source);

    void RemoveRange(long index, long count);
     
    IBigGenericArrayList<T> Repeat(T value, long count);
     
    void Reverse();
    void Reverse(long index, long count);

    void SetRange(long index, IBigCollection<T> collection);

    void Sort();
    void Sort(IComparer<T> comparer);
    void Sort(long index, long count, IComparer<T> comparer);
     
    IBigGenericArrayList<T> Synchronized(IBigGenericArrayList<T> source);
    IBigList<T> Synchronized(IBigList<T> source);
     
    BigArray<T> ToBigArray();
    void TrimToSize();
}