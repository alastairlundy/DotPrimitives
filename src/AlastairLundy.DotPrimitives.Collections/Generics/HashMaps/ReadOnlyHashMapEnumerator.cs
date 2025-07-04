/*
    AlastairLundy.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.DotPrimitives.Collections.Generics.HashMaps;

/// <summary>
/// An enumerator for a read-only hash map that provides access to its key-value pairs.
/// </summary>
/// <typeparam name="TKey">The type of keys in the hash map.</typeparam>
/// <typeparam name="TValue">The type of values in the hash map.</typeparam>
public struct ReadOnlyHashMapEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>> 
    where TKey : notnull
{
    private readonly ReadOnlyHashMap<TKey, TValue> _hashMap;

    /// <summary>
    /// Gets an array of keys that can be used to enumerate the values in this hash map.
    /// </summary>
    private IList<TKey> Keys { get; }

    private int _position = -1;
    
    /// <summary>
    /// Initializes a new instance of the Enumerator with the specified read-only hash map.
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
    /// <returns>True if there are more items to enumerate; otherwise, false.</returns>
    public bool MoveNext()
    {
        _position++;

        return (_position < _hashMap.Count);
    }

    /// <summary>
    /// Resets the position of this enumerator back to its initial value.
    /// </summary>
    public void Reset()
    {
        _position = -1;
    }

    /// <summary>
    /// Gets the current item in the sequence.
    /// </summary>
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
    /// Releases any resources used by this instance of the enumerator.
    /// </summary>
    /// <remarks>The default implementation disposes of the internal keys, but derived classes should override this method to release any resources that
    /// they own.</remarks>
    public void Dispose()
    {
        Keys.Clear();
    }
}