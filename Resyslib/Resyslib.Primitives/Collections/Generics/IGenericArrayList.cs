namespace AlastairLundy.Resyslib.Primitives.Collections.Generics;

public interface IGenericArrayList<T> : IList<T>, ICloneable
{
     bool IsFixedSize { get; }

     bool IsSynchronized { get; }
    
     int Capacity { get; }
    
     void AddRange(ICollection<T> collection);
     
     int BinarySearch(int index, int count, T value, IComparer<T> comparer); 
     int BinarySearch(T value, IComparer<T> comparer);

     void CopyTo(T[] array);
     void CopyTo(int index, T[] array, int arrayIndex, int count);
     
     IGenericArrayList<T> FixedSize(IGenericArrayList<T> source);
     IList<T> FixedSize(IList<T> source);
     
     IGenericArrayList<T> GetRange(int index, int count);

     int IndexOf(T? value, int startIndex);
     int IndexOf(T? value, int startIndex, int count);

     void InsertRange(int index, ICollection<T> collection);
     
     int LastIndexOf(T value);
     int LastIndexOf(T value, int startIndex);
     int LastIndexOf(T value, int startIndex, int count);
     
     IGenericArrayList<T> ReadOnly(IGenericArrayList<T> source);
     IList<T> ReadOnly(IList<T> source);

     void RemoveRange(int index, int count);
     
     IGenericArrayList<T> Repeat(T value, int count);
     
     void Reverse();
     void Reverse(int index, int count);

     void SetRange(int index, ICollection<T> collection);

     void Sort();
     void Sort(IComparer<T> comparer);
     void Sort(int index, int count, IComparer<T> comparer);
     
     IGenericArrayList<T> Synchronized(IGenericArrayList<T> source);
     IList<T> Synchronized(IList<T> source);
     
     T[] ToArray();
     void TrimToSize();
}