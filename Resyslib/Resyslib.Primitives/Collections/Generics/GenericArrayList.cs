using System.Collections;
using System.Diagnostics.Contracts;

// ReSharper disable RedundantBoolCompare
// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.Resyslib.Primitives.Collections.Generics;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericArrayList<T> : IGenericArrayList<T>
{
    private int _itemsToRemove;
    
    private const int DefaultInitialCapacity = 10;
    private KeyValuePair<T, bool>[] _items;

    private int _capacity;
    private int _count;
    
    private readonly bool _isReadOnly;
    private readonly bool _isFixedSize;
    
    private readonly bool _isSynchronized;

    protected GenericArrayList(bool isReadOnly, bool isFixedSize, bool isSynchronized, int capacity)
    {
        _capacity = capacity;
        _count = 0;
        _itemsToRemove = 0;
        
        _isReadOnly = isReadOnly;
        _isFixedSize = isFixedSize;
        _isSynchronized = isSynchronized;

        if (IsSynchronized)
        {

        }
        else
        {
            _items = new KeyValuePair<T, bool>[capacity];
        }
    }
    
    protected GenericArrayList(bool isReadOnly, bool isFixedSize, bool isSynchronized, int capacity, ICollection<T> items)
    {
        _capacity = capacity;
        _count = 0;
        _itemsToRemove = 0;
        
        _isReadOnly = isReadOnly;
        _isFixedSize = isFixedSize;
        _isSynchronized = isSynchronized;

        if (_isFixedSize)
        {
            
        }
        else
        {
            _items = new KeyValuePair<T, bool>[capacity];
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public GenericArrayList()
    {
        _items = new KeyValuePair<T, bool>[DefaultInitialCapacity];
        _capacity = DefaultInitialCapacity;
        _count = 0;
        _itemsToRemove = 0;
        
        _isReadOnly = false;
        _isFixedSize = false;
        _isSynchronized = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collection"></param>
    public GenericArrayList(ICollection<T> collection)
    {
        _items = new KeyValuePair<T, bool>[collection.Count + DefaultInitialCapacity];
        _capacity = collection.Count + DefaultInitialCapacity;
        _itemsToRemove = 0;
        
        _count = collection.Count;
        _isReadOnly = false;
        _isFixedSize = false;
        _isSynchronized = false;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="capacity"></param>
    public GenericArrayList(int capacity)
    {
        _items = new KeyValuePair<T, bool>[capacity];
        _capacity = capacity;
        _itemsToRemove = 0;
        _count = 0;
        
        _isReadOnly = false;
        _isFixedSize = false;
        _isSynchronized = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isFixedSize"></param>
    public GenericArrayList(bool isFixedSize)
    {
        _isFixedSize = isFixedSize;
        _items = new KeyValuePair<T, bool>[DefaultInitialCapacity];
        _capacity = DefaultInitialCapacity;
        _itemsToRemove = 0;
        _count = 0;
        
        _isReadOnly = false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new GenericArrayListEnumerator<T>(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private void CheckIfResizeRequired()
    {
        if (_itemsToRemove >= 10)
        {
            TrimToSize();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public void Add(T item)
    {
        if (IsFixedSize)
        {
            return;
        }
        
        if (_capacity > Count)
        {
            _items[Count + 1] = new KeyValuePair<T, bool>(item, false);
            _count++;
        }
        else
        {
            KeyValuePair<T, bool>[] oldItems = new KeyValuePair<T, bool>[Count];
            
            KeyValuePair<T, bool>[] newItems = new KeyValuePair<T, bool>[Count + DefaultInitialCapacity];
            
            Array.Copy(_items, 0, oldItems, 0, Count);

            _items = newItems;
            
            _items[Count + 1] = new KeyValuePair<T, bool>(item, false);
            _count++;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < Count; i++)
        {
            _items[i] = new KeyValuePair<T, bool>(_items[i].Key, true);
        }

        _capacity = DefaultInitialCapacity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Contains(T item)
    {
        foreach (KeyValuePair<T, bool> t in _items)
        {
            if (t.Value == false && t.Key != null &&  t.Key.Equals(item))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    public void CopyTo(T[] array, int arrayIndex)
    {
        TrimToSize();
        
        Array.Copy(_items, arrayIndex, array, 0, Count);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Remove(T item)
    {
        try
        {
            RemoveAt(IndexOf(item));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public int Count => _count;
    public int Capacity => _capacity;
    
    public void AddRange(ICollection<T> collection)
    {
        if (IsFixedSize)
        {
            return;
        }
        
    }

    public int BinarySearch(int index, int count, T value, IComparer<T> comparer)
    {
       
    }

    public int BinarySearch(T value, IComparer<T> comparer)
    {
       
    }

    public void CopyTo(T[] array)
    {
        
    }

    public void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    [Pure]
    public IGenericArrayList<T> FixedSize(IGenericArrayList<T> source)
    {
        return new GenericArrayList<T>(false, true, false, source.Count, source);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    [Pure]
    public IList<T> FixedSize(IList<T> source)
    {
        return new GenericArrayList<T>(false, true, false, source.Count, source);
    }

    public IGenericArrayList<T> GetRange(int index, int count)
    {
        
    }

    public int IndexOf(T? value, int startIndex)
    {
        
    }

    public int IndexOf(T? value, int startIndex, int count)
    {
        
    }

    public void InsertRange(int index, ICollection<T> collection)
    {
        
    }

    public int LastIndexOf(T value)
    {
        
    }

    public int LastIndexOf(T value, int startIndex)
    {
        
    }

    public int LastIndexOf(T value, int startIndex, int count)
    {
        
    }

    public IGenericArrayList<T> ReadOnly(IGenericArrayList<T> source)
    {
        
    }

    public IList<T> ReadOnly(IList<T> source)
    {
        
    }

    public void RemoveRange(int index, int count)
    {
        
    }

    public IGenericArrayList<T> Repeat(T value, int count)
    {
        
    }

    public void Reverse()
    {
        
    }

    public void Reverse(int index, int count)
    {
        
    }

    public void SetRange(int index, ICollection<T> collection)
    {
        
    }

    public void Sort()
    {
        
    }

    public void Sort(IComparer<T> comparer)
    {
        
    }

    public void Sort(int index, int count, IComparer<T> comparer)
    {
        
    }

    public IGenericArrayList<T> Synchronized(IGenericArrayList<T> source)
    {
        
    }

    public IList<T> Synchronized(IList<T> source)
    {
        
    }

    
    public T[] ToArray()
    {
        TrimToSize();
        
        return _items.Select(x => x.Key).ToArray();
    }

    /// <summary>
    /// 
    /// </summary>
    public void TrimToSize()
    {
        Array.Resize(ref _items, _capacity);
    }

    public bool IsFixedSize => _isFixedSize;
    public bool IsSynchronized => _items.IsSynchronized;
    public bool IsReadOnly => _isReadOnly;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int IndexOf(T item)
    {
        for (int i = 0; i < Count; i++)
        {
            if (_items[i].Key != null && _items[i].Key.Equals(item))
            {
                return i;
            }
        }
        
        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="item"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void Insert(int index, T item)
    {
        if (index > Capacity && index == Count + 1)
        {
            Add(item);
        }
        else if(index < Capacity)
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Index was out of range. Index cannot be negative.");
            }

            ShiftItemsUpOne(index);
            _items[index] = new KeyValuePair<T, bool>(item, false);
        }
    }

    private void ShiftItemsUpOne(int indexStart)
    {
        if (Count < Array.MaxLength)
        {
            for (int i = Count + 1; i >= indexStart ; i--)
            {
                _items[i] = _items[i - 1];
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void RemoveAt(int index)
    {
        if ((_count * 2) < _capacity)
        {
            TrimToSize();
        }
        else
        {
            _items[index] = new KeyValuePair<T, bool>(_items[index].Key, false);
            
            CheckIfResizeRequired();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public T this[int index]
    {
        get
        {
            if (_items[index].Value == true)
            {
                int i = index;
                
                while (i < Count)
                {
                    if (_items[i].Value == false)
                    {
                        return _items[i].Key;
                    }

                    i++;
                }
                
                return _items[i].Key;
            }
            else
            {
                return _items[index].Key;   
            }
        }
        set => _items[index] = new KeyValuePair<T, bool>(value, false);
    }

    public object Clone()
    {
        return this;
    }
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
internal class GenericArrayListEnumerator<T> : IEnumerator<T>
{
    private int _position = -1;
    
    private readonly GenericArrayList<T> _list;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="list"></param>
    public GenericArrayListEnumerator(GenericArrayList<T> list)
    {
        this._list = list;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool MoveNext()
    {
        _position++;
        
        return (_position < _list.Count);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Reset()
    {
        _position = -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="Exception"></exception>
    public T Current
    {
        get
        {
            try
            {
                return _list[_position];
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }

    object? IEnumerator.Current => Current;

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        _list.Clear();
    }
}