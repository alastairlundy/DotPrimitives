/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
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


namespace AlastairLundy.Resyslib.Primitives.Collections.Generics.HashMaps
{
    public interface IHashMap<TKey, TValue> : IReadOnlyHashMap<TKey, TValue>
    {
        int Count { get; }

        TValue GetValue(TKey key);
        TValue GetValueOrDefault(TKey key, TValue defaultValue);
        
        bool ContainsKey(TKey key);
        bool ContainsValue(TValue value);
        bool ContainsKeyValuePair(KeyValuePair<TKey, TValue> pair);
        
        bool IsEmpty { get; }
        
        void Put(TKey key, TValue value);
        void Put(KeyValuePair<TKey, TValue> pair);

        void PutIfAbsent(TKey key, TValue value);
        void PutIfAbsent(KeyValuePair<TKey, TValue> pair);
        
        bool Remove(TKey key);
        bool Remove(KeyValuePair<TKey, TValue> pair);

        void RemoveInstancesOf(TValue value);

        bool Replace(TKey key, TValue value);
        bool Replace(TKey key, TValue oldValue, TValue newValue);

        void Clear();
    }
}