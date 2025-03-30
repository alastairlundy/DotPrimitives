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

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.Resyslib.Collections.Generics.HashMaps
{
    /// <summary>
    /// An enumerator for a read-only hash map that provides access to its key-value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the hash map.</typeparam>
    /// <typeparam name="TValue">The type of values in the hash map.</typeparam>
    public class ReadOnlyHashMapEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>> 
        where TKey : notnull
    {
        private readonly ReadOnlyHashMap<TKey, TValue> _hashMap;

        /// <summary>
        /// Gets an array of keys that can be used to enumerate the values in this hash map.
        /// </summary>
        public TKey[] Keys { get; set; }

        private int _position = -1;
    
        /// <summary>
        /// Initializes a new instance of the class with the specified read-only hash map.
        /// </summary>
        /// <param name="hashMap">The read-only hash map to enumerate.</param>
        public ReadOnlyHashMapEnumerator(ReadOnlyHashMap<TKey, TValue> hashMap)
        {
            _hashMap = hashMap;
            Keys = _hashMap.Keys().ToArray();
        }
    
        /// <summary>
        /// Advances the enumerator to the next position in the sequence. If the current position is a valid 
        /// position for this instance of the enumerator, it moves the position to the next item and returns true.
        /// </summary>
        /// <returns>true if there are more items to enumerate; otherwise, false.</returns>
        public bool MoveNext()
        {
            _position++;

            return (_position < _hashMap.Count);
        }

        /// <summary>
        /// Resets the position of this instance of the enumerator back to its initial value.
        /// </summary>
        public void Reset()
        {
            _position = -1;
        }

        /// <summary>
        /// Gets the current item in the sequence. This value is the result of an implicit conversion 
        /// from an IEnumerator.</summary>
        public KeyValuePair<TKey, TValue> Current
        {
            get
            {
                TKey key = Keys[_position];
                TValue value = _hashMap.GetValue(key);
            
                return new KeyValuePair<TKey, TValue>(key, value);
            }
        }

        /// <summary>
        /// Implement the IEquatable interface to allow for equality comparisons between enumerators.
        /// </summary>
        object? IEnumerator.Current => Current;

        /// <summary>
        /// Releases any resources used by this instance of the enumerator. The default implementation
        /// disposes of the internal keys, but derived classes should override this method to release any resources that
        /// it owns.
        /// </summary>
        public void Dispose()
        {
            Keys = [];
        }
    }

}