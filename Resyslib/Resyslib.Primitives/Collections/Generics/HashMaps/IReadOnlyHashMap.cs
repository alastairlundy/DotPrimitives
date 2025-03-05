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
    /// <summary>
    /// The interface for a Read Only HashMap, that does not permit writing to it after instantiation.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IReadOnlyHashMap<TKey, TValue>
    {
        /// <summary>
        /// Gets an IEnumerable of all Keys in the HashMap.
        /// </summary>
        /// <returns>The IEnumerable of Keys in the HashMap.</returns>
        IEnumerable<TKey> Keys();
        
        /// <summary>
        /// Gets an IEnumerable of all Values in the HashMap.
        /// </summary>
        /// <returns>The IEnumerable of Values in the HashMap.</returns>
        IEnumerable<TValue> Values();
        
        /// <summary>
        /// Returns an IEnumerable of Key Value Pairs in the HashMap.
        /// </summary>
        /// <returns>An IEnumerable of KeyValuePairs in the HashMap.</returns>
        IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs();
    
        /// <summary>
        /// Returns the contents of the HashMap instantiated within an IReadonlyDictionary.
        /// </summary>
        /// <returns>The new IReadOnlyDictionary instance.</returns>
        IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary();
        
        IDictionary<TKey, TValue> ToDictionary();
    }
}