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
    /// Provides a way to iterate through a HashMap.
    /// </summary>
    /// <typeparam name="TKey">The type of key stored in the HashMap.</typeparam>
    /// <typeparam name="TValue">The type of value associated with each key.</typeparam>
    public class HashMapEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>> where TKey : notnull
    {
        /// <summary>
        /// The underlying HashMap being iterated.
        /// </summary>
        private readonly HashMap<TKey, TValue> _hashMap;

        /// <summary>
        /// The current position in the iteration, starting at -1.
        /// </summary>
        private int _position = -1;
    
        /// <summary>
        /// Gets the array of keys for the underlying HashMap.
        /// </summary>
        private TKey[] Keys { get; set; }
    
    
        /// <summary>
        /// Initializes a new instance of the <see cref="HashMapEnumerator{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="hashMap">The underlying HashMap to iterate.</param>
        public HashMapEnumerator(HashMap<TKey, TValue> hashMap)
        {
            _hashMap = hashMap;
            Keys = _hashMap.Keys().ToArray();
        }
    
        /// <summary>
        /// Advances the current position to the next key in the iteration.
        /// </summary>
        /// <returns>True if there is a next key, false otherwise.</returns>
        public bool MoveNext()
        {
            _position++;

            return (_position < _hashMap.Count);
        }

        /// <summary>
        /// Resets the current position to the beginning of the iteration.
        /// </summary>
        public void Reset()
        {
            _position = -1;
        }

        /// <summary>
        /// Gets the next key-value pair in the iteration.
        /// </summary>
        /// <returns>The next key-value pair.</returns>
        public KeyValuePair<TKey, TValue> Current
        {
            get
            {
                TValue value = _hashMap.GetValue(Keys[_position]);

                return new KeyValuePair<TKey, TValue>(Keys[_position], value);
            }
        }
    
        /// <summary>
        /// Implements the <see cref="System.Collections.IEnumerator.Current"/> property.
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Releases any resources held by the enumerator.
        /// </summary>
        public void Dispose()
        {
            Keys = [];
            _hashMap.Clear();
        }
    }

}